using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{

    public interface IObjectFactory<From,To>
    {
        To Create(From obj);
        From CreateDb(To obj);
    }
    public class ObjectFactory<From, To> : IObjectFactory<From, To>
    {

        public ObjectFactory()
        {
            Init();
        }

        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<From, To>();
                cfg.CreateMap<To, From>();
            });
            _mapper = config.CreateMapper();
        }

        public To Create(From obj)
        {
            return _mapper.Map<From, To>(obj);
        }

        public From CreateDb(To obj)
        {
            return _mapper.Map<To, From>(obj);
        }
    }
}
