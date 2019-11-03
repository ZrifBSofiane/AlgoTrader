using Reposiroty.DataAccess;
using Service.DTO;
using Service.Factory;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Classes
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _reposiroty = new ProductRepository();
        private readonly IProductFactory _factory = new ProductFactory();

        public bool AddForex(string baseCurr, string assetCurr, string market, string name, decimal pip, decimal margin)
        {
            return _reposiroty.AddForex(baseCurr, assetCurr, market, name, pip, margin);
        }

        public List<ProductDTO> Get()
        {
            var result = _reposiroty.Get();
            return result.Select(p => _factory.Create(p)).ToList();
        }

        public ProductDTO Get(string productName)
        {
            return _factory.Create(_reposiroty.Get(productName));
        }

        public List<ProductDTO> GetFx()
        {
            var result = _reposiroty.GetFx();
            return result.Select(p => _factory.Create(p)).ToList();
        }

        public int GetIdProduct(string name)
        {
            return _reposiroty.GetIdProduct(name);
        }
    }
}
