using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankUserAccountManagmentApplicationDAL.Repositories;
using BankUserAccountManagmentDAL.Helpers;
using BankUserAccountManagmentApplicationDAL.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BankUserAccountManagementApplication.Controllers
{
    public class AccountController : BaseController 
    {
        private UserRepository userRepository;
        public AccountController(IBaseRepository baseRepository, IHttpContextAccessor httpContextAccessor) : base(baseRepository, httpContextAccessor)
        {
            userRepository = new UserRepository(baseRepository);
        }


        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password))
            {

                UserProfileDTO userProfile = this.userRepository.GetUserByNameAndPassword(email, password);

                if (userProfile != null) {

                    var principal = new ClaimsPrincipal(ClaimHelpers.ConstructClaimsIdentity(userProfile));

                    HttpContext.Authentication.SignInAsync("Cookies", principal);
                    if (returnUrl != null)
                    {
                        if (returnUrl != null && returnUrl.Contains("Home/") || returnUrl.Contains("Users/Edit/0"))
                        {
                            return RedirectToAction("Edit", "Users", new { @id = ClaimHelpers.GetUserIDClaimValue((ClaimsIdentity)principal.Identity) });
                        }

                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult AccessDenied(string returnUrl = null)
        {
            return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
        }

        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("Cookies");
            return Redirect("/");
        }
    }
}