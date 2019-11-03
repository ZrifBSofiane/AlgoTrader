using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMapping
{
    public class TransactionMapping<T>
    {
        private static readonly Dictionary<T, HashSet<TransactionDTO>> _connections =
            new Dictionary<T, HashSet<TransactionDTO>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, TransactionDTO transaction)
        {
            lock (_connections)
            {
                HashSet<TransactionDTO> transactions;
                if (!_connections.TryGetValue(key, out transactions))
                {
                    transactions = new HashSet<TransactionDTO>();
                    _connections.Add(key, transactions);
                }

                lock (transactions)
                {
                    if(transactions.Where(t => t.Id == transaction.Id).Count() <= 0)
                        transactions.Add(transaction);
                }
            }
        }
        
        public IEnumerable<TransactionDTO> GetConnections(T key)
        {
            HashSet<TransactionDTO> transactions;
            if (_connections.TryGetValue(key, out transactions))
            {
                return transactions;
            }

            return Enumerable.Empty<TransactionDTO>();
        }

        public void Remove(T key, int dealId)
        {
            lock (_connections)
            {
                HashSet<TransactionDTO> transactions;
                if (!_connections.TryGetValue(key, out transactions))
                {
                    return;
                }

                lock (transactions)
                {
                    transactions.RemoveWhere(d => d.Id == dealId);

                    if (transactions.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
        public TransactionDTO Get(T key, int dealId)
        {
            lock (_connections)
            {
                HashSet<TransactionDTO> transactions;
                if (!_connections.TryGetValue(key, out transactions))
                {
                    return null;
                }

                lock (transactions)
                {
                    return transactions.FirstOrDefault(d => d.Id == dealId);
                }
            }
        }
    }
}
