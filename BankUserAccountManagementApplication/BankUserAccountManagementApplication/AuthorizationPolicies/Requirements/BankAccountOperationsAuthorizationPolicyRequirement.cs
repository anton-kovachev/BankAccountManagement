using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.AuthorizationPolicies.Requirements
{
    public class BankAccountOperationsAuthorizationPolicyRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; }
        public BankAccountOperationsAuthorizationPolicyRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
