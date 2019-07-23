using Reposiroty.Config;
using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
                    return context.Account.Include("User").Where(r => r.User.Id == id.ToString()).FirstOrDefault();
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

        public bool UpdateSignalRId(string idUuserName, string idSignalR)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.Where(r => r.User.UserName == idUuserName).FirstOrDefault();
                    if (user != null)
                        user.SignalRId = idSignalR;
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

        public bool UpdateSignalRId(Guid idUser, string idSignalR)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.Where(r => r.User.Id == idUser.ToString()).FirstOrDefault();
                    if (user != null)
                        user.SignalRId = idSignalR;
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

        public bool BlockOrUnBlockAccount(Guid idUser)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Account.FirstOrDefault(r => r.User.Id == idUser.ToString());
                    if (user != null)
                    {
                        if (user.User.IsBlocked.Value) // if already blocked => allow
                            user.User.IsBlocked = false;
                        else
                            user.User.IsBlocked = true;
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

        public string GetSignalRIdByUser(Guid idUser)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Database.SqlQuery<string>("Select SignalRId from Accounts acc JOIN AspNetUsers usr On acc.User_Id = usr.Id Where usr.Id = @id", new SqlParameter("@id", idUser.ToString())).FirstOrDefault();
                    return user;
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
        public string GetSignalRIdByUser(string username)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Database.SqlQuery<string>("Select SignalRId from Accounts acc JOIN AspNetUsers usr On acc.User_Id = usr.Id Where usr.UserName = @id", new SqlParameter("@id", username)).FirstOrDefault();
                    return user;
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
        public List<string> GetSignalRIdByUser(List<Guid> idUser)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Database.SqlQuery<string>("Select SignalRId from Accounts acc JOIN AspNetUsers usr On acc.User_Id = usr.Id Where usr.Id in (@id)", new SqlParameter("@id", idUser)).ToList<string>();
                    return user;
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
        public List<string> GetSignalRIdByUser(List<string> username)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Database.SqlQuery<string>("Select SignalRId from Accounts acc JOIN AspNetUsers usr On acc.User_Id = usr.Id Where usr.UserName in (@id)", new SqlParameter("@id", username)).ToList<string>();
                    return user;
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
    }
}
