using BankUserAccountManagementApplication.AuthorizationPolicies.Requirements;
using BankUserAccountManagmentDAL.Constants;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.AuthorizationPolicies
{
    public class AdminRoleHandlerPolicy : AuthorizationHandler<AdminAuthorizationPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthorizationPolicyRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypesCustom.Role))
            {
                return Task.CompletedTask;
            }

            if ( context.User.Claims.Any( c => c.Value == requirement.RoleName ) )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
