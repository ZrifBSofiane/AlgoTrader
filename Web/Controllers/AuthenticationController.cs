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

namespace Web.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {


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
                        return RedirectToAction("ConfirmEmail");
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
            return View();
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

                var resultRole = await UserManager.AddToRoleAsync(newUser.Id, newUser.Role.ToString());
                if(resultRole.Succeeded)
                {
                    goto SaveRole;
                }
                else
                {
                    var resultCreation = UserManager.IdentityManager.CreateRole(newUser.Role.ToString());
                    if(resultCreation)
                    {
                        goto SaveRole;
                    }
                }
                SaveRole:
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Authentication", new { userId = newUser.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(newUser.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\""
                    + callbackUrl + "\">link</a>");
                    return RedirectToAction("ConfirmEmail");

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

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            ViewBag.IsRedirected = false;
            if (string.IsNullOrWhiteSpace(userId))
                ViewBag.IsRedirected = true;
            else
            {
                await UserManager.ConfirmEmailAsync(userId, code);
            }
            return View();
        }



    }
}
