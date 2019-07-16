using Reposiroty.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.DataAccess
{
    public class UserRepository
    {

        public List<ApplicationUser> Get()
        {
            try
            {
                using(var context = new AlgoDbContext())
                {
                    return context.Users.ToList();
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

        public ApplicationUser Get(String IdOrUsename, bool isUsername)
        {
            if (isUsername)
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Users.FirstOrDefault(u => u.UserName == IdOrUsename);
                }
            }
            else
            {
                using (var context = new AlgoDbContext())
                {
                    return context.Users.FirstOrDefault(u => u.Id == IdOrUsename);
                }
            }

        }
        public bool UpdateLastConnection(string usernameOrId, bool isUsername)
        {
            if (isUsername)
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.UserName == usernameOrId);
                    user.LastConnection = DateTime.Now;
                    var count = context.SaveChanges();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                using (var context = new AlgoDbContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == usernameOrId);
                    user.LastConnection = DateTime.Now;
                    var count = context.SaveChanges();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
            }

        }



    }
}
