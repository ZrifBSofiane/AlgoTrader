using BusMapping;
using Management.Interfaces;
using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Service
{
    public class Pnl : IPnl
    {

        private readonly ProductMapping<string> _productMapping = new ProductMapping<string>();


        public void UpdatePnl(ref List<TransactionDTO> transactions, double bid, double ask)
        {
            transactions.ForEach(t =>
            {
                var factor = _productMapping.GetFactor(t.Product.Name);
                if (t.Way == Enums.Way.BUY.ToString())
                    t.PnL = (decimal)((decimal)bid - t.StartPrice);
                else
                    t.PnL = (decimal)(t.StartPrice - (decimal)ask);
                t.PnL = t.PnL * factor;
            });
        }
    }
}
