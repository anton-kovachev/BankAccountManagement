using BankUserAccountManagementApplication.AuthorizationPolicies.Requirements;
using BankUserAccountManagmentDAL.Constants;
using BankUserAccountManagmentDAL.Enums;
using BankUserAccountManagmentDAL.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.AuthorizationPolicies
{
    public class ViewEditResourceHandlerPolicy : AuthorizationHandler<UserProfileAuthorizationPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserProfileAuthorizationPolicyRequirement requirement)
        {
            var mvcContext = context.Resource as
                        Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            string idParam = mvcContext.HttpContext.Request.Path.Value.Split(new char[] { '/' }).Last();

            if (!context.User.HasClaim(c => c.Type == ClaimTypesCustom.UserID))
            {
                return Task.CompletedTask;
            }

            if (!context.User.HasClaim(c => c.Type == ClaimTypesCustom.Role))
            {
                return Task.CompletedTask;
            }

            if (idParam != null && ClaimHelpers.GetUserIDClaimValue((ClaimsIdentity)context.User.Identity) == Int32.Parse(idParam) )
            {
                context.Succeed(requirement);
            }

            if(context.User.Claims.Any(c => c.Type == ClaimTypesCustom.Role && c.Value == RolesEnum.Admin.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
