using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public UserDto User { get; set; }
    }
}
