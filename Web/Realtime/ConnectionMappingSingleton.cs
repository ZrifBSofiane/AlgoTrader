using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Realtime
{
    public sealed class ConnectionMappingSingleton
    {
        private static ConnectionMappingSingleton instance = null;
        private static readonly object padlock = new object();

        private readonly Dictionary<string, HashSet<string>> _connections;

        ConnectionMappingSingleton()
        {
            _connections = new Dictionary<string, HashSet<string>>();
        }

        public static void Add(string key, string connectionId)
        {
            lock (Instance._connections)
            {
                HashSet<string> connections;
                if (!Instance._connections.ContainsKey(key))
                    Instance._connections.Add(key, new HashSet<string>());
                if (!Instance._connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    Instance._connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public static IEnumerable<string> GetConnections(string key)
        {
            HashSet<string> connections;
            if (Instance._connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public static void Remove(string key, string connectionId)
        {
            lock (Instance._connections)
            {
                HashSet<string> connections;
                if (!Instance._connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        Instance._connections.Remove(key);
                    }
                }
            }
        }

        public static void RemoveFromAll(string connectionId)
        {
            lock (Instance._connections)
            {
                List<string> keys = new List<string>();
                foreach(var temp in Instance._connections)
                {
                    if(temp.Value.Contains(connectionId))
                    {
                        keys.Add(temp.Key);
                    }
                }

                HashSet<string> connections;
                foreach (var temp in keys)
                {
                    if (!Instance._connections.TryGetValue(temp, out connections))
                    {
                        return;
                    }

                    lock (connections)
                    {
                        connections.Remove(connectionId);

                        if (connections.Count == 0)
                        {
                            Instance._connections.Remove(temp);
                        }
                    }
                }
            }
        }



        public static ConnectionMappingSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ConnectionMappingSingleton();
                        }
                    }
                }
                return instance;
            }
        }

       
    }
}