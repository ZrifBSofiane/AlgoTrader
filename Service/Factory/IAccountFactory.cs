using AutoMapper;
using Reposiroty.Config;
using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{
    public interface IAccountFactory
    {
        AccountDTO Create(Account acc);
        Account CreateDb(AccountDTO acc);
    }

    public class AccountFactory : IAccountFactory
    {
        public AccountFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>();
                cfg.CreateMap<AccountDTO, Account>();
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<UserDto, ApplicationUser > ();
            });
            _mapper = config.CreateMapper();
        }

        public AccountDTO Create(Account acc)
        {
            return _mapper.Map<Account, AccountDTO>(acc);
        }

        public Account CreateDb(AccountDTO acc)
        {
            return _mapper.Map<AccountDTO, Account>(acc);
        }
    }
}
