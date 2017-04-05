using BankUserAccountManagementApplication.ValidationAttributes;
using BankUserAccountManagmentDAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountWithdrawViewModel : BankAccountOperationBasicViewModel
    {
        [Range(0.01, AmountConstants.MaxAmount, ErrorMessage = " {0} must be between {1} and Current Amount !")]
        [Display(Name = "Amount to Withdraw ")]
        [DataType(DataType.Currency)]
        [LessOrEqualValueValidationAttribute("AmountCurrent")]
        public decimal AmountToWithdraw { get; set; }
    }
}
