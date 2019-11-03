using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel.PortalController
{
    public class PortalViewModel
    {
        public List<TransactionDTO> Transactions { get; set; }
    }
}