using Reposiroty.DataAccess;
using Reposiroty.Models;
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
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionFactory _factory = new TransactionFactory();
        private readonly TransactionRepository _repository = new TransactionRepository();

        public int Add(TransactionDTO tr)
        {
            return _repository.Add(_factory.CreateDb(tr));
        }

        public bool Close(Enums.StatusDeal statusDeal, decimal endPrice, int dealId, decimal pnl)
        {
            return _repository.Close(statusDeal, endPrice, dealId, pnl);
        }

        public TransactionDTO Get(int idDeal)
        {
            return _factory.Create(_repository.Get(idDeal));
        }

        public List<TransactionDTO> GetAllByUser(Guid idUser)
        {
            var result = _repository.GetAllByUser(idUser);
            return result.Select(t => _factory.Create(t)).ToList();
        }

        public List<TransactionDTO> GetAllByUserByProductName(Guid idUser, string productName)
        {
            var result = _repository.GetAllByUserByProductName(idUser, productName);
            return result.Select(t => _factory.Create(t)).ToList();
        }

        public List<TransactionDTO> GetLiveDeal(string userId)
        {
            var result = _repository.GetLiveDealByUser(userId);
            return result.Select(t => _factory.Create(t)).ToList();
        }

        public Dictionary<string, double> GetStatisticForex(string userId)
        {
            return _repository.GetStatisticForex(userId);
        }
    }
}
