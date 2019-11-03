using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel.PlatformController
{
    public class OpenDealVM
    {
        public string Ticker { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Slippage { get; set; }
    }
}