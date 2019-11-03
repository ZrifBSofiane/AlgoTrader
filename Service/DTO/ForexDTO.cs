using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class ForexDTO
    {
        public int Id { get; set; }
        public String Asset { get; set; }
        public String Base { get; set; }
        public decimal Pip { get; set; }
        public decimal MarginPercentage { get; set; }

        public string GetDescription()
        {
            return $"Factor Pip x{Pip}, Margin risk {MarginPercentage}%";
        }
    }
}
