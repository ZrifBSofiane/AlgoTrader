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
    public class Dico<T1,T2>
    {
        public T1 Key { get; set; }
        public T2 Value { get; set; }
    }

    public class TransactionRepository
    {

        public List<Transaction> GetAllByUser(Guid idUser)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Transactions.Where(u => u.User.Id == idUser.ToString())
                        .Include(p => p.Product)
                        .Include(p => p.Product.Forex).ToList();
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

        public Transaction Get(int idDeal)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Transactions.Where(u => u.Id == idDeal)
                        .Include(p => p.Product)
                        .Include(p => p.Product.Forex).FirstOrDefault();
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

        public List<Transaction> GetAllByUserByProductName(Guid idUser, string productName)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Transactions.Where(u => u.User.Id == idUser.ToString() && u.Product.Name == productName).ToList();
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

        public List<Transaction> GetLiveDealByUser(string userId)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Transactions.Where(u => u.User.Id == userId && u.Statuts == Enums.StatusDeal.Opened.ToString())
                        .Include(p => p.User)
                        .Include(p => p.Product)
                        .Include(p => p.Product.Forex).ToList();
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

        public bool Close(Enums.StatusDeal statusDeal, decimal endPrice, int dealId, decimal pnl)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new AlgoDbContext())
                {

                    var parameter = new List<SqlParameter>()
                    {
                        new SqlParameter("@dealStatus", statusDeal.ToString()),
                        new SqlParameter("@endDate", DateTime.UtcNow),
                        new SqlParameter("@endPrice", endPrice),
                        new SqlParameter("@dealId", dealId),
                        new SqlParameter("@PnL", pnl)
                    };
                    var curr = context.Database.ExecuteSqlCommand("Update Transactions Set Statuts = @dealStatus , EndDate = @endDate, EndPrice = @endPrice, PnL = @PnL Where Id = @dealId", parameter.ToArray());
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

        /// <summary>
        /// Add transaction 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns>Return the id of the transaction or -1 if error</returns>
        public int Add(Transaction tr)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {

                    var parameter = new List<SqlParameter>()
                    {
                        new SqlParameter("@Way", tr.Way),
                        new SqlParameter("@StartDate", tr.StartDate),
                        new SqlParameter("@EndDate", tr.EndDate),
                        new SqlParameter("@Statuts", tr.Statuts),
                        new SqlParameter("@StartPrice", tr.StartPrice),
                        new SqlParameter("@EndPrice", tr.EndPrice),
                        new SqlParameter("@Product_Id", tr.Product.Id),
                        new SqlParameter("@User_Id", tr.User.Id),

                    };
                    var curr = context.Database.SqlQuery<int>("INSERT INTO Transactions OUTPUT Inserted.ID VALUES (@Way, @StartDate, @EndDate, @Statuts, @StartPrice, @EndPrice, @Product_Id, @User_Id, 0)", parameter.ToArray()).FirstOrDefault();
                    context.SaveChanges();
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

        

        public Dictionary<string, double> GetStatisticForex(string userId)
        {
            try
            {
                using (var context = new AlgoDbContext())
                {
                    var parameter = new List<SqlParameter>()
                    {
                        new SqlParameter("@UserId", userId),
                    };

                    var curr = context.Database.SqlQuery<Dico<string,double>>("  Select p.[Name] , Count(t.Id)  from Transactions as t Inner Join Products as p On t.Product_Id = p.Id Where t.User_Id = @UserId Group by p.[Name]", parameter.ToArray());
                    return curr.ToDictionary(r => r.Key, r => r.Value);
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
            return null;
        }







    }
}
