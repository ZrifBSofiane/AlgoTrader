using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        List<ProductDTO> Get();
        bool AddForex(string baseCurr, string assetCurr, string market, string name, decimal pip, decimal margin);
        int GetIdProduct(string name);
        ProductDTO Get(string productName);
        List<ProductDTO> GetFx();
    }
}
