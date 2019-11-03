using DataHistoricalRepository.DataAccess;
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
    public class HistoricalDataService : IHistoricalDataService
    {
        private readonly HistoricalDataRepository  _reposiroty = new HistoricalDataRepository();
        private readonly IHistoricalDataFactory _factory = new HistoricalDataFactory();

        public List<HistoricalDataDTO> Get(string product)
        {
            var result = _reposiroty.GetData(product);
            return result.Select(d => _factory.Create(d)).ToList();
        }
    }
}
