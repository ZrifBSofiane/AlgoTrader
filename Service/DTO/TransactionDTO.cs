using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public String Way { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Statuts { get; set; }
        public decimal StartPrice { get; set; }
        public decimal EndPrice { get; set; }
        public UserDto User { get; set; }
        public ProductDTO Product { get; set; }
    }
}
