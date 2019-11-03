using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Interfaces
{
    public interface IPnl
    {
        void UpdatePnl(ref List<TransactionDTO> transactions, double bid, double ask);
    }
}
