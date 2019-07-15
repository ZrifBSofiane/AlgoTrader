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

        public AdminController(IUserService userService)
        {
            _userService = userService;
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
    }
}