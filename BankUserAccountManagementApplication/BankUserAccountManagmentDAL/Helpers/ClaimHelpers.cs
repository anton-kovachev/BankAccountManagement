using BankUserAccountManagmentApplicationDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using BankUserAccountManagmentDAL.Constants;
using BankUserAccountManagmentDAL.Enums;

namespace BankUserAccountManagmentDAL.Helpers
{
    public static class ClaimHelpers
    {
        public static ClaimsPrincipal ConstructClaimsIdentity(UserProfileDTO userProfile)
        {
            var claims = new List<Claim>
                        {
                             new Claim(ClaimTypesCustom.UserID, userProfile.ID.ToString()),
                             new Claim(ClaimTypesCustom.UserName, userProfile.Email.ToString()),
                };

            userProfile.UserRoles.ForEach(ur => claims.Add(new Claim(ClaimTypesCustom.Role, ur.RoleName)));
            userProfile.UserRoles.ForEach(ur => claims.Add(new Claim(ClaimTypes.Role, ur.RoleName)));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "password");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            return principal;
        }

        public static int GetUserIDClaimValue(ClaimsIdentity claimsIdentity)
        {
            string userIdclaimValue = claimsIdentity.Claims.Single(c => c.Type == ClaimTypesCustom.UserID).Value;
            int userID = Int32.Parse(userIdclaimValue);

            return userID;
        }

        public static string GetUserNameClaimValue(ClaimsIdentity claimsIdentity)
        {
            string userNameClaimValue = claimsIdentity.Claims.Where(c => c.Type == ClaimTypesCustom.UserName).Select( c => c.Value ).FirstOrDefault();
         
            return userNameClaimValue;
        }

        public static IEnumerable<string> GetRoleClaimValues(ClaimsIdentity claimsIdentity)
        {
            IEnumerable<string> roleClaimValues = claimsIdentity.Claims.Where(c => c.Type == ClaimTypesCustom.Role).Select(c => c.Value);

            return roleClaimValues;
        }

        public static bool IsUserAnAdmin(ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.Claims.Any(c => c.Type == ClaimTypesCustom.Role && c.Value == RolesEnum.Admin.ToString());
        }
    }
}
