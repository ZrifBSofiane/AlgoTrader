using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHistoricalRepository.Models
{
    public class HistoricalData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public DateTime Date { get; set; }
        public long Time { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
