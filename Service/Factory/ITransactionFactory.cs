using AutoMapper;
using Reposiroty.Config;
using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{

    public interface ITransactionFactory
    {
        TransactionDTO Create(Transaction acc);
        Transaction CreateDb(TransactionDTO acc);
    }

    public class TransactionFactory : ITransactionFactory
    {

        private static readonly IProductFactory productFactory = new ProductFactory();

        public TransactionFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDTO>();
                cfg.CreateMap<TransactionDTO, Transaction>();
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<UserDto, ApplicationUser>();
                cfg.CreateMap<Forex, ForexDTO>();
                cfg.CreateMap<ForexDTO, Forex>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();

            });
            _mapper = config.CreateMapper();
        }

        public TransactionDTO Create(Transaction acc)
        {
            return _mapper.Map<Transaction, TransactionDTO>(acc);
        }

        public Transaction CreateDb(TransactionDTO acc)
        {
            return _mapper.Map<TransactionDTO, Transaction>(acc);
        }
    }
}
