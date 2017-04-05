using BankUserAccountManagmentDAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountOperationBasicViewModel
    {
        [Range(1, int.MaxValue)]
        [Display(Name = "Account ID:")]
        public int ID { get; set; }

        [Range(0, AmountConstants.MaxAmount, ErrorMessage = "Amount must be between {0}, {1} !")]
        [Display(Name = "Current Amount:")]
        [DataType(DataType.Currency)]
        public decimal AmountCurrent { get; set; }

        [Display(Name = "Owner:")]
        public string Owner { get; set; }

        [Display(Name = "Last modified on:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime ModifiedDate { get; set; }
    }
}
