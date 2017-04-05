using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankUserAccountManagmentApplicationDAL.Repositories;
using BankUserAccountManagmentDAL.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BankUserAccountManagementApplication.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IBaseRepository baseRepository, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                baseRepository.UserID = ClaimHelpers.GetUserIDClaimValue((ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity);
            }
        }
    }
}