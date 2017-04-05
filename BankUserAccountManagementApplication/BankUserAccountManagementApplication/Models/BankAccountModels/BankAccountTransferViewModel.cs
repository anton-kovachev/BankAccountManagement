using BankUserAccountManagementApplication.ValidationAttributes;
using BankUserAccountManagmentDAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountTransferViewModel : BankAccountOperationBasicViewModel
    {
        [Range(0.01, AmountConstants.MaxAmount, ErrorMessage = " {0} must be between {1} and Current Amount .")]
        [Display(Name = "Amount to Transfer")]
        [DataType(DataType.Currency)]
        [LessOrEqualValueValidationAttribute("AmountCurrent")]
        public decimal AmountToTransfer { get; set; }

        [Display(Name ="Account ID to transfer")]
        [Range(1, AmountConstants.MaxAmount, ErrorMessage = " {0} must be between {1} and {2}")]
        [NotEqualValueValidationAttribute("ID", ErrorMessage = " {0} must be different from current Account ID !")]
        public int? BankAccountIDToTransfer { get; set; } 
    }
}
