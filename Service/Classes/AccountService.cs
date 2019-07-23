using Reposiroty.Config;
using Reposiroty.DataAccess;
using Reposiroty.Models;
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
    public class AccountService : IAccountService
    {
        private readonly IAccountFactory _factory = new AccountFactory();
        private readonly IObjectFactory<ApplicationUser, UserDto> _factoryUser = new ObjectFactory<ApplicationUser, UserDto>();
        private readonly AccountRepository _repository = new AccountRepository();

        public bool BlockOrUnBlockAccount(Guid idUser)
        {
            return _repository.BlockOrUnBlockAccount(idUser);
        }

        public List<AccountDTO> Get()
        {
            List<Account> result = _repository.Get();
            return result.Select(r => _factory.Create(r)).ToList();
        }

        public AccountDTO Get(Guid id)
        {
            return _factory.Create(_repository.Get(id));
        }

        public AccountDTO Get(string email)
        {
            return _factory.Create(_repository.Get(email));
        }

        public string GetSignalRIdByUser(Guid idUser)
        {
            return _repository.GetSignalRIdByUser(idUser);
        }

        public string GetSignalRIdByUser(string username)
        {
            return _repository.GetSignalRIdByUser(username);
        }

        public List<string> GetSignalRIdByUser(List<Guid> idUser)
        {
            return _repository.GetSignalRIdByUser(idUser);
        }

        public List<string> GetSignalRIdByUser(List<string> username)
        {
            return _repository.GetSignalRIdByUser(username);
        }

        public bool UpdateAmount(decimal amount, UserDto User)
        {
            return _repository.UpdateAmount(amount, _factoryUser.CreateDb(User));
        }

        public bool UpdateAmount(decimal amount, Guid id)
        {
            return _repository.UpdateAmount(amount, id);
        }

        public bool UpdateAmount(decimal amount, string email)
        {
            return _repository.UpdateAmount(amount, email);
        }

        public bool UpdateSignalRId(Guid idUser, string idSignalR)
        {
            return _repository.UpdateSignalRId(idUser, idSignalR);
        }

        public bool UpdateSignalRId(string idUuserName, string idSignalR)
        {
            return _repository.UpdateSignalRId(idUuserName, idSignalR);
        }
    }
}
