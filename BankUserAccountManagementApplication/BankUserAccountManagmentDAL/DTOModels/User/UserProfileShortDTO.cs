using BankUserAccountManagmentDAL.DTOModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.DTOModels.User
{
    public class UserProfileShortDTO : UserBasicDTO
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } 

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public string CreatedByUser { get; set; }

        [Required]
        public int BankAccountID { get; set; }
    }
}
