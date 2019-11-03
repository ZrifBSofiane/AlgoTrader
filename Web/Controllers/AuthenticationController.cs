using Microsoft.AspNet.Identity;
using Reposiroty;
using Service.DTO;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Web;
using System.Threading.Tasks;
using Reposiroty.Models;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using Service.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthenticationController(IAccountService account)
        {
            _accountService = account;
        }



        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserDto user, string returnUrl = null)
        {
             var _user = await UserManager.FindByEmailAsync(user.Email);
            if (_user != null)
            {
                if (UserManager.CheckPasswordAsync(_user, user.Password).Result)
                {
                    if (UserManager.IsEmailConfirmedAsync(_user.Id).Result)
                    {
                        var resultRole = await UserManager.AddToRoleAsync(_user.Id, Enums.Role.SuperAdmin.ToString());
                        await SignInManager.SignInAsync(_user, false, false);
                        //_userService.UpdateLastConnection(_user.Id, false); TODO
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("GetConfirmEmail", new {userId = _user.Id});
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or password incorrect. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("", "This email is not registred in our database.");
                return View();
            }

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newUser = new Reposiroty.Config.ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Name = user.Name, // Has to be unique => considered as Username for the client
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                DateBirth = user.DateBirth,
                UserName = user.Name,
                Role = Enums.Role.User,
            };
            var result = await UserManager.CreateAsync(newUser, newUser.Password);
            if (result.Succeeded)
            {
                var accountCreation = _accountService.CreateAccount(newUser.Id);
                var resultRole = await UserManager.AddToRoleAsync(newUser.Id, newUser.Role.ToString());
                if(resultRole.Succeeded)
                {
                    return RedirectToAction("GetConfirmEmail", new { userId = newUser.Id });
                }
                else
                {
                    var resultCreation = UserManager.IdentityManager.CreateRole(newUser.Role.ToString());
                    if(resultCreation)
                    {
                        await UserManager.AddToRoleAsync(newUser.Id, newUser.Role.ToString());
                        return RedirectToAction("GetConfirmEmail", new { userId = newUser.Id });
                    }
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        public async Task<ActionResult> GetConfirmEmail(string userId)
        {
            ViewBag.IsRedirected = false;
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Action("PostConfirmEmail", "Authentication", new { userId = userId, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userId, "Confirm your account", "Please confirm your account by clicking this link: <a href=\""
            + callbackUrl + "\">link</a>");
            ViewBag.Email = UserManager.GetEmail(userId);
            return View();
        }


        
        public async Task<ActionResult> PostConfirmEmail(string userId, string code)
        {
            ViewBag.IsRedirected = false;
            if (string.IsNullOrWhiteSpace(userId))
                ViewBag.IsRedirected = true;
            else
            {
                await UserManager.ConfirmEmailAsync(userId, code);
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
