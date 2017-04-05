using BankUserAccountManagementApplication.Models.UserProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountViewModel
    {
        [Range(1, int.MaxValue)]
        [Display(Name = "Account ID")]
        public int ID { get; set; }

        [Display(Name="Amount")]
        [DataType(DataType.Currency)]
        [Range(0, 10000000000,ErrorMessage = "Amount must be between {0} and {1} !")]
        public decimal Amount { get; set; }

        [Display(Name ="Owner:")]
        public UserProfileBasicViewModel Owner { get; set; }

        [Display(Name = "Created by:")]
        public UserProfileBasicViewModel CreatedByUser { get; set; }

        [Display(Name = "Created on:")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified by:")]
        public UserProfileBasicViewModel ModifiedByUser { get; set; }

        [Display(Name="Last Modified on: ")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime ModifiedDate { get; set; }

        public IEnumerable<BankAccountAuditViewModel> BankAccountAuditList { get; set; }
    }
}
