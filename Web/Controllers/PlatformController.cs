using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel.PlatformController;

namespace Web.Controllers
{
    public class PlatformController : Controller
    {
        // GET: Platform
        public ActionResult Index()
        {
            PlatformVM viewModel = new PlatformVM();
            viewModel.Ticker = "GBPUSD";
            return View(viewModel);
        }
    }
}