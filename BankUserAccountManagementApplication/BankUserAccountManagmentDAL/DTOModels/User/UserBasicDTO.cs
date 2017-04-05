using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankUserAccountManagmentDAL.DTOModels.User
{
    public class UserBasicDTO
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
