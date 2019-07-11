using AutoMapper;
using Reposiroty.Config;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{
    public interface IUserFactory
    {
        UserDto Create(ApplicationUser user);
    }

    public class UserFactory : IUserFactory
    {

        public UserFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<UserDto, ApplicationUser>();
            });
            _mapper = config.CreateMapper();
        }

        public UserDto Create(ApplicationUser user)
        {
            return _mapper.Map<ApplicationUser, UserDto>(user);
        }
    }
}
