using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.DataAccess
{
    public class ForexRepository
    {


        public bool Exist(string baseCurr, string assetCurr)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var curr = context.Forex.FirstOrDefault(f => f.Asset == assetCurr && f.Base == baseCurr);
                    if (curr == null)
                    {
                        return false;
                    }
                    else
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



        public List<Forex> Get()
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Forex.ToList();
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

        public List<Forex> Get(string assetBase, bool isAsset)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Forex.Where(f => isAsset ? f.Asset == assetBase : f.Base == assetBase).ToList();
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

        public bool Add(string assetCurr, string baseCurr)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var curr = context.Forex.FirstOrDefault(f => f.Asset == assetCurr && f.Base == baseCurr);
                    if(curr == null)
                    {
                        var newFX = new Forex()
                        {
                            Asset = assetCurr.ToUpper(),
                            Base = baseCurr.ToUpper(),
                        };
                        context.Forex.Add(newFX);
                        context.SaveChanges();
                    }
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

        public bool Remove(string assetCurr, string baseCurr)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {

                    var newFX = new Forex()
                    {
                        Asset = assetCurr.ToUpper(),
                        Base = baseCurr.ToUpper(),
                    };
                    var curr = context.Forex.Remove(newFX);
                    if (curr != null)
                    {
                        context.SaveChanges();
                    }
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
