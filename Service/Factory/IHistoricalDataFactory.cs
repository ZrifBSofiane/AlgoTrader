using AutoMapper;
using DataHistoricalRepository.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{
    public interface IHistoricalDataFactory
    {
        HistoricalDataDTO Create(HistoricalData acc);
    }


    public class HistoricalDataFactory : IHistoricalDataFactory
    {
        public HistoricalDataFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HistoricalData, HistoricalDataDTO>();
                cfg.CreateMap<HistoricalDataDTO, HistoricalData>();
            });
            _mapper = config.CreateMapper();
        }

        public HistoricalDataDTO Create(HistoricalData acc)
        {
            return _mapper.Map<HistoricalData, HistoricalDataDTO>(acc);
        }

        public HistoricalData CreateDb(HistoricalDataDTO acc)
        {
            return _mapper.Map<HistoricalDataDTO, HistoricalData>(acc);
        }
    }
}
