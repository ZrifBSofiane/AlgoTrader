using Reposiroty.Models;
using Service.Factory;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel.ProductController;

namespace Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IForexService _forexService;
        private readonly IProductService _productService;
        public ProductController(IForexService forexService, IProductService productService)
        {
            _forexService = forexService;
            _productService = productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _productService.Get();
            return View(products);
        }


        public JsonResult GetProducts()
        {
            var products = _productService.Get();

            var jsonResult = new object[products.Count];

            for (int i = 0; i < products.Count; i++)
            {
                jsonResult[i] = new
                {
                    type = products[i].Type,
                    name = products[i].Name,
                    market = products[i].Market,
                    product = products[i].Type == "FOREX" ? products[i].Forex : products[i].Forex,
                };
            }
            return Json(new
            {
                data = jsonResult,
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddProduct(NewProductViewModel model)
        {
            var isOk = false;
            switch (model.Type.Trim().ToUpper())
            {
                case "FOREX": // change with ProductType Enum
                    isOk = _productService.AddForex(model.Param1, model.Param2, model.Market, model.Name, Convert.ToDecimal(model.Param3, System.Globalization.CultureInfo.InvariantCulture), Convert.ToDecimal(model.Param4, System.Globalization.CultureInfo.InvariantCulture));
                    break;
            }


            return Json(new
            {
                isOk = isOk,
                message = "ok"
            });
        }

        [HttpGet]
        public PartialViewResult GetAddProduct(string productType)
        {
            switch (productType.Trim().ToUpper())
            {
                case "FOREX":
                    {
                        var r = PartialView("~/Views/Product/AddProduct/PartialAddForex.cshtml");
                        return r;
                    }

            }
            return null;

        }
    }
}