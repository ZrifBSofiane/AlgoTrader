using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel.PlatformController
{
    public class CloseDealVm
    {
        public double EndPrice { get; set; }
        public int DealId { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
    }
}