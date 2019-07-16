using Reposiroty.Config;
using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.DataAccess
{
    public class AccountRepository
    {
        public List<Account> Get()
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Account.ToList();
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

        public Account Get(Guid id)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Account.Include("User").Where(r => r.User.Id == id.ToString()) .FirstOrDefault();
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

        public Account Get(string email)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Account.Where(r => r.User.Email == email).FirstOrDefault();
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

        public bool UpdateAmount(decimal amount, ApplicationUser User)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.Where(r => r.User == User).FirstOrDefault();
                    if (user != null)
                        user.Amount = amount;
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

        public bool UpdateAmount(decimal amount, Guid id)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.Where(r => r.User.Id == id.ToString()).FirstOrDefault();
                    if (user != null)
                        user.Amount = amount;
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

        public bool UpdateAmount(decimal amount, string email)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.Where(r => r.User.Email == email).FirstOrDefault();
                    if (user != null)
                        user.Amount = amount;
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
