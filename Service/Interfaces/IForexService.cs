using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IForexService
    {
        List<ForexDTO> Get();
        List<ForexDTO> Get(string assetBase, bool isAsset);
        bool Add(string assetCurr, string baseCurr);
        bool Remove(string assetCurr, string baseCurr);
    }
}
