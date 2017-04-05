using BankUserAccountManagementApplication.ValidationAttributes;
using BankUserAccountManagmentDAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountDepositViewModel : BankAccountOperationBasicViewModel
    {
        [Range(0.01, AmountConstants.MaxAmount, ErrorMessage = " {0} must be between {1} and {2} !")]
        [Display(Name = "Amount to Deposit")]
        [DataType(DataType.Currency)]
        [ExceedMaxValueValidation("AmountCurrent")]
        public decimal AmountToDeposit { get; set; }
    }
}
