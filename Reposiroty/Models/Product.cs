using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Market { get; set; }
        public String Type { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual Forex Forex { get; set; }
    }
}
