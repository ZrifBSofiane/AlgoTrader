using Reposiroty.Config;
using Reposiroty.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
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
                    return context.Account
                        .Include(t => t.User)
                        .ToList();
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
                    return context.Account.Where(r => r.User.Email == email)
                        .Include(t => t.User)
                        .FirstOrDefault();
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

        public bool CreateAccount(string idUser)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var startAmount = context.Parameters.FirstOrDefault(p => p.Key == "StartAmount");
                    if (startAmount == null)
                        return false;
                    var user = context.Users.FirstOrDefault(d => d.Id == idUser);
                    if (user == null)
                        return false;
                    var newAccount = new Account()
                    {
                        Amount = Convert.ToInt32(startAmount.Value),
                        User = user,
                    };
                    context.Account.Add(newAccount);
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

        public bool AddPnL(string userId, double pnl)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new AlgoDbContext())
                {

                    var parameter = new List<SqlParameter>()
                    {
                        new SqlParameter("@idUser", userId),
                        new SqlParameter("@pnl", pnl)
                    };
                    var curr = context.Database.ExecuteSqlCommand("Update Accounts set Amount = Amount + @pnl Where User_Id = @idUser", parameter.ToArray());
                    context.SaveChanges();
                    return true;
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
            return false;
        }



    }
}
