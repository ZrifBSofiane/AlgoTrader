using Microsoft.AspNet.Identity.EntityFramework;
using Reposiroty.Config;


namespace Reposiroty
{
    public class AlgoDbContext : IdentityDbContext<ApplicationUser>
    {
        public AlgoDbContext() : base("AlgoEntity", false)
        {

        }
    }
}
