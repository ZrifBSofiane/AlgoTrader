using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Web.Realtime
{

    public class ProductRealTime
    {
        public string Name { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public DateTime Time { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Open { get; set; }
    }


    public class PriceRealTime<T>
    {
        private static readonly Dictionary<T, ProductRealTime> _connections =
            new Dictionary<T, ProductRealTime>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void AddOrUpdate(T key, ProductRealTime newPrice)
        {
            lock (_connections)
            {
                ProductRealTime products;
                if (!_connections.TryGetValue(key, out products))
                {
                    products = newPrice;
                    _connections.Add(key, products);
                }
                else
                {
                    _connections[key] = newPrice;
                    Debug.WriteLine($"update Close {newPrice.Close} Date {newPrice.Time}");
                }
                    
            }
        }

        public ProductRealTime Get(T key)
        {
            ProductRealTime product;
            if (_connections.TryGetValue(key, out product))
            {
                return product;
            }

            return null;
        }


    }
}