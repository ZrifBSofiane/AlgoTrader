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
    public class UserService : IUserService
    {
        private readonly IUserFactory _factory = new UserFactory();
        private readonly UserRepository _repository = new UserRepository();

        public List<UserDto> Get()
        {
            var result = _repository.Get();
            return result.Select(r => _factory.Create(r)).ToList();
        }


        public UserDto Get(String usernameOrId, bool isUsername)
        {
            var result = _repository.Get(usernameOrId, isUsername);
            return _factory.Create(result);
        }

        public bool UpdateLastConnection(string userNameOrId, bool isUsername)
        {
            return _repository.UpdateLastConnection(userNameOrId, isUsername);
        }
    }
}
