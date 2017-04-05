using BankUserAccountManagementApplication.AuthorizationPolicies.Requirements;
using BankUserAccountManagmentDAL.Constants;
using BankUserAccountManagmentDAL.Enums;
using BankUserAccountManagmentDAL.Helpers;
using BankUserAccountManagmentDAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.AuthorizationPolicies
{
    public class BankAccountOperationsHandlePolicy : AuthorizationHandler<BankAccountOperationsAuthorizationPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BankAccountOperationsAuthorizationPolicyRequirement requirement)
        {
            var mvcContext = context.Resource as
                      Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            string idParam = mvcContext.HttpContext.Request.Path.Value.Split(new char[] { '/' }).Last();

            var id = mvcContext.ActionDescriptor.Id;

            BankAccountRepository bankAccountRepo = new BankAccountRepository((int)UserEnum.SystemUser);

            int accountOwnerUserID = bankAccountRepo.GetBankAccountOwnerId(Int32.Parse(idParam));

            if (accountOwnerUserID == ClaimHelpers.GetUserIDClaimValue((ClaimsIdentity)context.User.Identity))
            {
                context.Succeed(requirement);
            }

            if (context.User.Claims.Any(c => c.Value == requirement.RoleName))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
