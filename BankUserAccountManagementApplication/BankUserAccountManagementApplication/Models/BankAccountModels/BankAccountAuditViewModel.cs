using BankUserAccountManagementApplication.Models.UserProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.BankAccountModels
{
    public class BankAccountAuditViewModel
    {
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        [Display(Name="Previous Amount")]
        [DataType(DataType.Currency)]
        public decimal PreviousAmount { get; set; }

        [Display(Name="Current Amount")]
        [DataType(DataType.Currency)]
        public decimal CurrentAmount { get; set; }

        [Display(Name = "Amount Change")]
        [DataType(DataType.Currency)]
        public decimal ChangeAmount { get; set; }

        [Display(Name="Created by")]
        public UserProfileBasicViewModel CreatedByUser { get; set; }

        [Display(Name = "Created on")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Account Operation")]
        public string BankAccountOperation { get; set; }

        [Display(Name = "Target Account ID")]
        public int? TranserBankAccountID { get; set; }

    }
}
