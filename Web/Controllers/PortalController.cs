using Microsoft.AspNet.Identity;
using Service.Classes;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class PortalController : Controller
    {
        private readonly ITransactionService _transactionService;

        public PortalController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTrade()
        {
            var idUser = User.Identity.GetUserId();
            var trades = _transactionService.GetAllByUser(new Guid(idUser));

            return PartialView("PartialListTrade", trades);
        }

        public ActionResult TradeStatisticsForex() // add percentage or number
        {
            var idUser = User.Identity.GetUserId();
            var trades = _transactionService.GetStatisticForex(idUser);
            return PartialView("PartialListTrade", trades);
        }

        [HttpGet]
        public FileResult GenerateAndDownloadStatement()
        {

            byte[] result = null;
            TradingPdfService.BuildTradingPdf(User.Identity.GetUserId(), ref result);


            // return result;

            return File(result, "application/pdf");
        }
    }
}