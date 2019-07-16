using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAccountService
    {
         List<AccountDTO> Get();
         AccountDTO Get(Guid id);
         AccountDTO Get(string email);
         bool UpdateAmount(decimal amount, UserDto User);
         bool UpdateAmount(decimal amount, Guid id);
         bool UpdateAmount(decimal amount, string email);


    }
}
