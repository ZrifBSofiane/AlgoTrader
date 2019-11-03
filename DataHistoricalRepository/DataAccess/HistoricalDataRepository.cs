using DataHistoricalRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHistoricalRepository.DataAccess
{
    public class HistoricalDataRepository
    {


        public bool AddData(HistoricalData data)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new DataHistoContext())
                {
                    data.DateAdded = DateTime.UtcNow;
                    context.Datas.Add(data);
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


        public bool AddDataAsync(HistoricalData data)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new DataHistoContext())
                {
                    data.DateAdded = DateTime.UtcNow;
                    context.Datas.Add(data);
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

        public List<HistoricalData> GetData(string productName, DateTime start, DateTime end)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new DataHistoContext())
                {
                    var result = context.Datas.Where(d => d.Date >= start && d.Date < end && d.Name == productName).ToList();
                    return result;
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


        public List<HistoricalData> GetData(string productName)
        {
            //Update Transactions Set Statuts = 'Closed', EndDate = GETDATE(), EndPrice = 32.98 Where Id = 33
            try
            {
                using (var context = new DataHistoContext())
                {
                    var result = context.Database.SqlQuery<HistoricalData>("Select TOP(500) [Id], [Name], [Name], [Open], [High], [Low], [Close], [Date], firstRes.[Time], [DateAdded] FROM [dbo].[HistoricalData] as firstRes INNER JOIN (Select max(DateAdded) as Added, [Id] as idSub, [Time] FROM [dbo].[HistoricalData] group by [Time], [Id]) as secRes On firstRes.Id = secRes.idSub Where firstRes.[Name] = @productName order by firstRes.[Time] DESC", new SqlParameter("@productName", productName )).ToList();
                    return result;
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
