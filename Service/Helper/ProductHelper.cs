using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public static class ProductHelper
    {
        public static string GetInfo(this ProductDTO p)
        {
            var type = p.Type;
            switch(type)
            {
                case "FOREX":
                    return p.ToString();
                default:
                    return "Product type undifined. Mapping not found";
            }
        }
    }
}
