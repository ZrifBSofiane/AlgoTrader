using Reposiroty.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public String Way { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Statuts { get; set; }
        public decimal StartPrice { get; set; }
        public decimal EndPrice { get; set; }
        public decimal PnL { get; set; }

        //Foreign Key
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
