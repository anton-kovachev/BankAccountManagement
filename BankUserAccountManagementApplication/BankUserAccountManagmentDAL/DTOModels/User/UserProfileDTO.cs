using BankUserAccountManagmentApplicationDAL.DTOModels.Role;
using BankUserAccountManagmentApplicationDAL.DTOModels.User;
using BankUserAccountManagmentApplicationDAL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.ViewModels
{
    public class UserProfileDTO : UserProfileShortDTO
    {
        [Required]
        [MaxLength(500)]
        public string Address { get; set; }
        [Required]
        [MaxLength(50)]
        public string PassportNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public List<UserRoleDTO> UserRoles { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
