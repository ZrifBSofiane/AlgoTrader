using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Reposiroty.Models;
using static Reposiroty.Models.Enums;

namespace Reposiroty.Config
{
    public class ApplicationUser : IdentityUser
    {
        public String Code { get; set; }
        public Role Role { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Password { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? LastConnection { get; set; }
        public bool? IsBlocked { get; set; }

        //Foreign
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Progression> Progressions { get; set; }
    }



}
