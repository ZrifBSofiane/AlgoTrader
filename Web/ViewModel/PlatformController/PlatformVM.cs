using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel.PlatformController
{
    public class PlatformVM : LayoutViewModel
    {

        public string IdAccount { get; set; }
        public double FinalAmount { get; set; } // Capital + closed / opened deals
        public double Capital { get; set; }
        public double GainLoss { get; set; }
        public double MarginUsed { get; set; }
        public double MarginFree { get; set; }

        public string Ticker { get; set; }
        public string Asset { get; set; }
        public string Base { get; set; }

        public TransactionDTO TransactionModel { get; set; }
        
    }
}