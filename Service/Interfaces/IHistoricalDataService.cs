using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IHistoricalDataService
    {
        List<HistoricalDataDTO> Get(string product);
    }
}
