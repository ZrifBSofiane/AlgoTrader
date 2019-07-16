using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.Models
{
    [Table("Forex")]
    public class Forex
    {
        [Key]
        public int Id { get; set; }
        public String Asset { get; set; }
        public String Base { get; set; }
    }
}
