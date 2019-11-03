using BusMapping;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Reposiroty.Models;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Realtime;
using Web.ViewModel;
using Web.ViewModel.PlatformController;

namespace Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class PlatformController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly IForexService _forexService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IHistoricalDataService _historicalDataService;
        private readonly static TransactionMapping<string> _transactionsMapping = new TransactionMapping<string>();
        private readonly static PriceRealTime<string> _priceMapping = new PriceRealTime<string>();
        private readonly static ProductMapping<string> _productMapping = new ProductMapping<string>();

        public PlatformController(IForexService forexService, IHistoricalDataService historicalDataService, IAccountService account, ITransactionService transaction, IProductService product, IUserService user)
        {
            _transactionService = transaction;
            _productService = product;
            _userService = user;
            _accountService = account;
            _historicalDataService = historicalDataService;
            _forexService = forexService;
        }



        // GET: Platform
        public ActionResult Index(string product)
        {
            PlatformVM viewModel = new PlatformVM();
            if(string.IsNullOrWhiteSpace(product))
            {
                viewModel.Ticker = "EURUSD";
                viewModel.Asset = "EUR";
                viewModel.Base = "USD";
            }
            else
            {
                viewModel.Ticker = product;
                viewModel.Asset = string.Join("",product.Take(3));
                viewModel.Base = string.Join("", product.Skip(3).Take(3));
            }
            
            var idUser = User.Identity.GetUserId();
            var account = _accountService.Get(new Guid(idUser));
            if (account != null)
            {
                viewModel.Capital = (double)account.Amount;
                viewModel.IdAccount = account.Id.ToString();
            }



            return View(viewModel);
        }

        public ActionResult FxWatch()
        {

            var productFx = _productService.GetFx();
            ViewBag.Products = productFx;
            return View(new LayoutViewModel());
        }

        public ActionResult TradeBlotter()
        {
            return View();
        }


        public JsonResult GetTransactionByTicker()
        {
            var isOk = true;
            var message = string.Empty;
            var idUser = User.Identity.GetUserId();
            object[] jsonResult = null;
            if (string.IsNullOrWhiteSpace("EURGBP"))
            {
                isOk = false;
                message = "Product name null or empty";
            }
            else if (string.IsNullOrWhiteSpace(idUser))
            {
                isOk = false;
                message = "IdUser name null or empty";
            }
            else
            {
                var transacs = _transactionService.GetAllByUser(new Guid(idUser));
                jsonResult = new object[transacs.Count];

                for (int i = 0; i < transacs.Count; i++)
                {
                    jsonResult[i] = new
                    {
                        id = transacs[i].Id,
                        name = transacs[i].Product.Name,
                        startDate = transacs[i].StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        endDate = transacs[i].EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        startPrice = transacs[i].StartPrice,
                        endPrice = transacs[i].EndPrice,
                        pnl = transacs[i].PnL,
                        way = transacs[i].Way,
                        status = transacs[i].Statuts,
                    };
                }

            }
            return Json(new
            {
                //isOk = isOk,
                //message = message,
                data = jsonResult,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetHistoricalData(string productName)
        {
            var isOk = true;
            var message = string.Empty;
            object[] jsonResult = null;
            if (string.IsNullOrWhiteSpace(productName))
            {
                isOk = false;
                message = "Product name null or empty, cannot get Historical";
            }
            else
            {
                var histo = _historicalDataService.Get(productName);
                jsonResult = new object[histo.Count];

                for (int i = 0; i < histo.Count; i++)
                {
                    jsonResult[i] = new
                    {
                        time = histo[i].Time,
                        open = histo[i].Open,
                        high = histo[i].High,
                        low = histo[i].Low,
                        close = histo[i].Close,
                    };
                }

            }
            return Json(new
            {
                isOk = isOk,
                message = isOk ? "Historical retrived" : message,
                data = jsonResult,
            }, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult OpenDeal(string json)
        {

            var serializer = new JavaScriptSerializer();
            dynamic jsondata = serializer.Deserialize(json, typeof(OpenDealVM));
            var model = new OpenDealVM()
            {
                Price = jsondata.Price,
                Quantity = jsondata.Quantity,
                Slippage = jsondata.Slippage,
                Ticker = jsondata.Ticker,
            };
            var isOk = true;
            var message = string.Empty;
            var idUser = User.Identity.GetUserId();
            var product = _productService.Get(model.Ticker);
            TransactionDTO transaction = null;
            if (string.IsNullOrWhiteSpace(idUser))
            {
                isOk = false;
                message = "Unable to get IdUser";
            }
            else if (product == null)
            {
                isOk = false;
                message = $"Product {model.Ticker} not found in database";
            }
            else
            {
                var actualPrice = _priceMapping.Get(model.Ticker);
                if (actualPrice == null)
                {
                    isOk = false;
                    message = $"Unable to get {model.Ticker} price";
                }
                else
                {
                    transaction = new TransactionDTO()
                    {
                        Way = model.Quantity > 0 ? Enums.Way.BUY.ToString() : Enums.Way.SELL.ToString(),
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        Statuts = Enums.StatusDeal.Opened.ToString(),
                        StartPrice = (decimal)(model.Quantity > 0 ? actualPrice.Ask : actualPrice.Bid),  // change here depending on slippage, you need to get first the price from broker
                        EndPrice = 0,
                        User = _userService.Get(User.Identity.GetUserId(), false),
                        Product = product,
                        PnL = 0,
                    };
                    var newId = _transactionService.Add(transaction);
                    if (newId > 0)
                    {
                        transaction.Id = newId;
                        _transactionsMapping.Add(model.Ticker, transaction);
                    }
                    else
                    {
                        isOk = false;
                        message = "Unable to create deal in database";
                    }
                }
            }

            return Json(new
            {
                isOk = isOk,
                message = isOk ? $"Deal {transaction?.Id} created" : message,
                transaction = transaction,
            });
        }

        [HttpPost]
        public JsonResult CloseDeal(string json)
        {
            decimal endPrice = 0;
            var serializer = new JavaScriptSerializer();
            dynamic jsondata = serializer.Deserialize(json, typeof(CloseDealVm));
            var model = new CloseDealVm()
            {
                DealId = jsondata.DealId,
                EndPrice = jsondata.EndPrice,
                Status = jsondata.Status,
                ProductName = jsondata.ProductName,
            };
            var isOk = true;
            var message = string.Empty;
            var idUser = User.Identity.GetUserId();
            if (string.IsNullOrWhiteSpace(idUser))
            {
                isOk = false;
                message = "Unable to get IdUser";
            }
            else
            {
                if(model.DealId == null || model.DealId < 0)
                {
                    isOk = false;
                    message = $"Id Deal incorrect, can't find it";
                }
                else
                {
                    var transac = _transactionService.Get(model.DealId);
                    var actualPrice = _priceMapping.Get(transac.Product.Name);
                    if (actualPrice == null)
                    {
                        isOk = false;
                        message = $"Unable to get {transac.Product.Name} price";
                    }
                    else
                    {
                        var transaction = _transactionsMapping.Get(transac.Product.Name, transac.Id);
                        if (transaction == null)
                        {
                            isOk = false;
                            message = $"Unable to find deal Id : {transac.Id} in mapping dico";
                        }
                        else
                        {
                            endPrice = (decimal)(transaction.Way == Enums.Way.BUY.ToString() ? actualPrice.Bid : actualPrice.Ask);
                            var result = _transactionService.Close(Enums.StatusDeal.Closed, endPrice, model.DealId, transaction.PnL);
                            _transactionsMapping.Remove(transac.Product.Name, transac.Id);
                            if (result == true) // TODO the returned result must be the id of the deal or -1 if not exist
                            {
                                isOk = true;
                                message = $"Deal {transac.Id} closed";
                            }
                        }
                    }
                }
            }
            

            return Json(new
            {
                isOk = isOk,
                message = message,
                newEndPrice = endPrice,
                newStatus = Enums.StatusDeal.Closed.ToString(),
                endDate = DateTime.UtcNow.ToString()
            }); ;
        }


        [HttpGet]
        public JsonResult GetProductMapping(string productName)
        {
            var isOk = true;
            var message = string.Empty;
            object[] jsonResult = new object[1];

            if (string.IsNullOrWhiteSpace(productName))
            {
                isOk = false;
                message = $"ProductName empty. Stop looking for mapping";
            }
            else
            {
                var product = _productMapping.Get(productName);
                if (product == null)
                {
                    isOk = false;
                    message = $"{productName} mapping not found. Must add product before";

                }
                else
                {

                    message = $"Mapping for {product.Name} from {product.Market} : ";
                    switch (product.Type)
                    {
                        case "FOREX":
                            message += product.Forex.GetDescription();
                            break;
                        default:
                            message += $"INFO not found, cause : Undefined product type ({product.Type})";
                            break;
                    }
                }
            }

            jsonResult[0] = new
            {
                hour = DateTime.UtcNow.ToString("dd'/'MM'/'yyyy HH:mm:ss"),
                message = message,
                type = "Mapping"
            };
            return Json(new
            {
                isOk = isOk,
                message = message,
                data = jsonResult

            }, JsonRequestBehavior.AllowGet);
        }




    }
}