using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.UserProfileModels
{
    public class UserProfileViewModel : UserProfileShortViewModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name="Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
