using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models.UserProfileModels
{
    public class UserProfileBasicViewModel
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

     
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
