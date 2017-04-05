using BankUserAccountManagmentDAL.Constants;
using BankUserAccountManagmentDAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserName(this ClaimsPrincipal User)
        {
            string userName = string.Empty;
            if (User != null && User.Claims.Any(c => c.Type == ClaimTypesCustom.UserName))
            {
              userName = User.Claims.Where(c => c.Type == ClaimTypesCustom.UserName).Select(c => c.Value).First();
            }

            return userName;
        }

        public static bool IsUserAdmin(this ClaimsPrincipal User)
        {
            bool isUserAdmin = false;
            if (User != null)
            {
                isUserAdmin = ClaimHelpers.IsUserAnAdmin((ClaimsIdentity)User.Identity);
            }

            return isUserAdmin;
        }

        public static int GetUserId(this ClaimsPrincipal User)
        {
            int userId = 0;
            if (User != null)
            {
                try
                {
                    userId = ClaimHelpers.GetUserIDClaimValue((ClaimsIdentity)User.Identity);
                }
                catch(Exception ex)
                {

                }
            }

            return userId;
        }
    }
}
