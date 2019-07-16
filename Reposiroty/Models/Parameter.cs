using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.Models
{
    public class Parameter
    {
        [Key]
        public int Id { get; set; }
        public bool IsMarketOpened { get; set; }
        public decimal RequiredMargin { get; set; }
        public decimal StartAmount { get; set; }
    }
}
