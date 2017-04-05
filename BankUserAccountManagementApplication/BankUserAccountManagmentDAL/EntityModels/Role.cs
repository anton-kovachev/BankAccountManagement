using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.EntityModels
{
    public class Role
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
