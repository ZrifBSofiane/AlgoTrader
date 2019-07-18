using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel.ProductController
{
    public class NewForexProductVM
    {
        public ForexDTO Forex { get; set; }
        public ProductDTO Product { get; set; }
    }
}