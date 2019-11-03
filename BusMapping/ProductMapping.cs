using Service.Classes;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMapping
{
    public class ProductMapping<T>
    {
        // TODO : change dico type to add ProductName and Market
        private static readonly Dictionary<T, HashSet<ProductDTO>> _products = new Dictionary<T, HashSet<ProductDTO>>();
        private static readonly IProductService _productService = new ProductService();

        public int Count
        {
            get
            {
                return _products.Count;
            }
        }

        public void Add(T key, ProductDTO product)
        {
            lock (_products)
            {
                HashSet<ProductDTO> products;
                if (!_products.TryGetValue(key, out products))
                {
                    products = new HashSet<ProductDTO>();
                    _products.Add(key, products);
                }

                lock (products)
                {
                    products.Clear();
                    products.Add(product);
                }
            }
        }


        public decimal GetFactor(T key)
        {
            var product = Get(key);
            if(product == null)
            {
                var products = _productService.Get();
                if (products.Count == 0)
                {
                    return 0.0M;
                }
                ProductMapping<string> temp = new ProductMapping<string>();
                foreach (var p in products)
                {

                    temp.Add(p.Name, p);
                }
                product = Get(key);
            }
            switch(product.Type)
            {
                case "FOREX":
                    return 1 / product.Forex.Pip;
                default:
                    return 1;
            }
        }

        public IEnumerable<ProductDTO> GetConnections(T key)
        {
            HashSet<ProductDTO> products;
            if (_products.TryGetValue(key, out products))
            {
                return products;
            }

            return Enumerable.Empty<ProductDTO>();
        }

        public void Remove(T key, int dealId)
        {
            lock (_products)
            {
                HashSet<ProductDTO> products;
                if (!_products.TryGetValue(key, out products))
                {
                    return;
                }

                lock (products)
                {
                    products.RemoveWhere(d => d.Id == dealId);

                    if (products.Count == 0)
                    {
                        _products.Remove(key);
                    }
                }
            }
        }
        public ProductDTO Get(T key)
        {
            lock (_products)
            {
                HashSet<ProductDTO> products;
                if (!_products.TryGetValue(key, out products))
                {
                    return null;
                }

                lock (products)
                {
                    return products.FirstOrDefault();
                }
            }
        }
    }
}
