using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.DataAccess
{
    public class ProductRepository
    {
        private readonly ForexRepository _forexRepo = new ForexRepository();


        public List<Product> Get()
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var test = context.Product.Include("Forex") // Include after all other Asset
                        .ToList();

                    return test;
                }
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public bool AddForex(string baseCurr, string assetCurr, string market, string name)
        {
            try
            {

                if (_forexRepo.Exist(baseCurr, assetCurr))
                    return true;

                using (var context = new AlgoDbContext())
                {
                    var newFx = new Forex()
                    {
                        Asset = assetCurr.ToUpper(),
                        Base = baseCurr.ToUpper(),
                    };
                    var newProduct = new Product()
                    {
                        Forex = newFx,
                        Market = market.ToUpper(),
                        Name = name.ToUpper(),
                    };
                    context.Product.Add(newProduct);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
