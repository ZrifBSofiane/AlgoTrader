using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Reposiroty.Config;


namespace Reposiroty
{
    public class AlgoDbContext : IdentityDbContext<ApplicationUser>
    {
        public AlgoDbContext() : base("AlgoEntity", false)
        {

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add here specifications like Precision of double or NULL / Default value
        }

        public static AlgoDbContext Create()
        {
            return new AlgoDbContext();
        }
    }

    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new AlgoDbContext()));
            return rm.RoleExists(name);
        }

        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new AlgoDbContext()));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new AlgoDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new AlgoDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }



    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var store = new UserStore<ApplicationUser>(context.Get<AlgoDbContext>());
            var manager = new ApplicationUserManager(store);
            // email unicity 
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = false,
            };
            // password criteria 
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // TOken providor 
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            // email service 
            manager.EmailService = new EmailService();
            return manager;
        }

        public IdentityManager IdentityManager
        {
            get { return new IdentityManager(); }
        }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }



    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            var credentialUserName = "contact@sofiane-zrif-bouragba.com";
            var sentFrom = "contact@sofiane-zrif-bouragba.com";
            var pwd = "password"; //TODO : change credentials

            // Configure the client:
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mi3-wss6.a2hosting.com"); // TODO : Change Server

            client.Port = 25;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            mail.IsBodyHtml = true;

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync(mail);
        }
    }


}
