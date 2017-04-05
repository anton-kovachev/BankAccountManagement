using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.UserProfileModels
{
    public class UserProfileShortAmountViewModel : UserProfileViewModel
    {
        [Required]
        [Range(0,10000000, ErrorMessage = "Amount must be between 0$ and 10000000.") ]
        [Display(Name = "Account Amount")]
        public decimal BankAccountAmount { get; set; }
    }
}
