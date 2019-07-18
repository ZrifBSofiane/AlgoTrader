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

        public bool AddForex(string baseCurr, string assetCurr, string market, string name)
        {
            return _reposiroty.AddForex(baseCurr, assetCurr, market, name);
        }

        public List<ProductDTO> Get()
        {
            var result = _reposiroty.Get();
            return result.Select(p => _factory.Create(p)).ToList();
        }
    }
}
