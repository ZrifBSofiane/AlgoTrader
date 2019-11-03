using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMapping
{
    public class MappingSignalRUserId
    {
        public string SignalRId { get; set; }
        public string UserId { get; set; }
    }


    public sealed class ConnectionMappingSingleton
    {
        private static ConnectionMappingSingleton instance = null;
        private static readonly object padlock = new object();

        private readonly Dictionary<string, HashSet<MappingSignalRUserId>> _connections;

        ConnectionMappingSingleton()
        {
            _connections = new Dictionary<string, HashSet<MappingSignalRUserId>>();
        }

        public static void Add(string key, string signalRId, string UserId)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            lock (Instance._connections)
            {
                HashSet<MappingSignalRUserId> connections;
                if (!Instance._connections.ContainsKey(key))
                    Instance._connections.Add(key, new HashSet<MappingSignalRUserId>());
                if (!Instance._connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<MappingSignalRUserId>();
                    Instance._connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(new MappingSignalRUserId()
                    {
                        UserId = UserId,
                        SignalRId = signalRId,
                    });
                }
            }
        }

        public static IEnumerable<MappingSignalRUserId> GetConnections(string key)
        {
            HashSet<MappingSignalRUserId> connections;
            if (Instance._connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<MappingSignalRUserId>();
        }

        public static void Remove(string key, string signalRId)
        {
            lock (Instance._connections)
            {
                HashSet<MappingSignalRUserId> connections;
                if (!Instance._connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.RemoveWhere(t => t.SignalRId == signalRId);

                    if (connections.Count == 0)
                    {
                        Instance._connections.Remove(key);
                    }
                }
            }
        }

        public static void RemoveFromAll(string signalRId)
        {
            lock (Instance._connections)
            {
                List<string> keys = new List<string>();
                foreach (var temp in Instance._connections)
                {
                    if (temp.Value.Where(t => t.SignalRId == signalRId).Count() > 0)
                    {
                        keys.Add(temp.Key);
                    }
                }

                HashSet<MappingSignalRUserId> connections;
                foreach (var temp in keys)
                {
                    if (!Instance._connections.TryGetValue(temp, out connections))
                    {
                        return;
                    }

                    lock (connections)
                    {
                        connections.RemoveWhere(t => t.SignalRId == signalRId);

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
