using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.DataAccess
{
    public class ProductRepository
    {
        private readonly ForexRepository _forexRepo = new ForexRepository();


        public int GetIdProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return -1;
            try
            {
                using (var context = new AlgoDbContext())
                {
                    int curr = context.Database.SqlQuery<int>("Select Id from Products Where Name = @name", new SqlParameter("@name", name)).FirstOrDefault();
                    return curr;
                }
            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e.Message);
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public List<Product> GetFx()
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var test = context.Product.Include("Forex")
                        .Where(t => t.Forex != null)// Include after all other Asset
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

        public Product Get(string productName)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var test = context.Product.Include("Forex").Where(p => p.Name==productName).FirstOrDefault();
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

        public bool AddForex(string baseCurr, string assetCurr, string market, string name, decimal pip, decimal margin)
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
                        MarginPercentage = margin,
                        Pip = pip,
                    };
                    var newProduct = new Product()
                    {
                        Forex = newFx,
                        Market = market.ToUpper(),
                        Name = name.ToUpper(),
                        Type = Enums.TypeProduct.FOREX.ToString().ToUpper(),
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
