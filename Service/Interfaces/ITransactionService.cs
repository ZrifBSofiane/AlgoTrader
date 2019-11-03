using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITransactionService
    {
        TransactionDTO Get(int idDeal);
        List<TransactionDTO> GetAllByUser(Guid idUser);
        List<TransactionDTO> GetAllByUserByProductName(Guid idUser, string productName);
        int Add(TransactionDTO tr);
        bool Close(Enums.StatusDeal statusDeal, decimal endPrice, int dealId, decimal pnl);
        Dictionary<string, double> GetStatisticForex(string userId);
        List<TransactionDTO> GetLiveDeal(string userId);
    }
}
