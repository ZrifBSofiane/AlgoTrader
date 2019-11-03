using BusMapping;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly static ProductMapping<string> _productMapping = new ProductMapping<string>();

        public AdminController(IProductService productService, IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
            _productService = productService;

            SetOrUpdateProductMapping();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialUser()
        {
            var users = _userService.Get();
            return PartialView("_UserIndex", users);
        }

        [HttpGet]
        public PartialViewResult GetDetailUser(string idUser)
        {
            var _account = _accountService.Get(new Guid(idUser));
            return PartialView("PartialUserDetails", _account);
        }

        [HttpPost]
        public JsonResult BlockOrUnBlockUser(string idUser)
        {
            var result = _accountService.BlockOrUnBlockAccount(new Guid(idUser));
            return Json(new
            {
                isOk = result,
                message = result ? "Account updated" : "An error happened",
            });
        }
        /// <summary>
        /// Set product mapping, in order to compute accurate pnl, and be able to know if connected
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult SetOrUpdateProductMapping()
        {
            var isOk = true;
            var message = string.Empty;
            var products = _productService.Get();
            if(products.Count == 0)
            {
                isOk = false;
                message = "Products lists is empty. Must add products before";
            }
            foreach(var p in products)
            {
                _productMapping.Add(p.Name, p);
            }
            return Json(new
            {
                isOk = isOk,
                message = isOk ? "Product mapping updated" : message,
            });
        }



    }
}