using AutoMapper;
using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{
    public interface IForexFactory
    {
        ForexDTO Create(Forex acc);
        Forex CreateDb(ForexDTO acc);
    }

    public class ForexFactory : IForexFactory
    {
        public ForexFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Forex, ForexDTO>();
                cfg.CreateMap<ForexDTO, Forex>();
            });
            _mapper = config.CreateMapper();
        }

        public ForexDTO Create(Forex acc)
        {
            return _mapper.Map<Forex, ForexDTO>(acc);
        }

        public Forex CreateDb(ForexDTO acc)
        {
            return _mapper.Map<ForexDTO, Forex>(acc);
        }
    }
}
