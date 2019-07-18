using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Market { get; set; }
        public String Type { get; set; }

        public ForexDTO Forex { get; set; }
    }
}
