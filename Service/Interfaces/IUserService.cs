using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        List<UserDto> Get();

        UserDto Get(String usernameOrId, bool isUsername);

        bool UpdateLastConnection(string userNameOrid, bool isUsername);

    }
}
