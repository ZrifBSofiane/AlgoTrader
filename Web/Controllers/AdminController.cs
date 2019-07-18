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

        public AdminController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult User()
        {
            var users = _userService.Get();
            return View(users);
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
    }
}