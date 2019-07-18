using AutoMapper;
using Reposiroty.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Factory
{
    public interface IProductFactory
    {
        ProductDTO Create(Product acc);
        Product CreateDb(ProductDTO acc);
    }

    public class ProductFactory : IProductFactory
    {
        public ProductFactory()
        {
            Init();
        }
        private static IMapper _mapper;

        private void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Forex, ForexDTO>();
                cfg.CreateMap<ForexDTO, Forex>();
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(s => s.Forex, opt => opt.MapFrom(d => d.Forex));
                cfg.CreateMap<ProductDTO, Product>();
                
                
            });
            _mapper = config.CreateMapper();
        }

        public ProductDTO Create(Product acc)
        {
            return _mapper.Map<Product, ProductDTO>(acc);
        }

        public Product CreateDb(ProductDTO acc)
        {
            return _mapper.Map<ProductDTO, Product>(acc);
        }
    }
}
