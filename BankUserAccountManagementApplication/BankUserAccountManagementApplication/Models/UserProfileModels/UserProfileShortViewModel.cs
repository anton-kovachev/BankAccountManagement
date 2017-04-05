using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.UserProfileModels
{
    public class UserProfileShortViewModel : UserProfileBasicViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByUser { get; set; }

        [Display(Name = "Account Number")]
        public int BankAccountID { get; set; }
    }
}
