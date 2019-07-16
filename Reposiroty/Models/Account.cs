using Reposiroty.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reposiroty.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }


        //Foreign Key
        public virtual ApplicationUser User { get; set; }

    }
}
