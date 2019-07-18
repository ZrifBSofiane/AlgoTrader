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
    public class ForexService : IForexService
    {
        private readonly IForexFactory _factory = new ForexFactory();
        private readonly ForexRepository _reposiroty = new ForexRepository();

        public bool Add(string assetCurr, string baseCurr)
        {
            return _reposiroty.Add(assetCurr, baseCurr);
        }

        public List<ForexDTO> Get()
        {
            var result = _reposiroty.Get();
            return result.Select(f => _factory.Create(f)).ToList();
        }

        public List<ForexDTO> Get(string assetBase, bool isAsset)
        {
            var result = _reposiroty.Get(assetBase, isAsset);
            return result.Select(f => _factory.Create(f)).ToList();
        }

        public bool Remove(string assetCurr, string baseCurr)
        {
            return _reposiroty.Remove(assetCurr, baseCurr);
        }
    }
}
