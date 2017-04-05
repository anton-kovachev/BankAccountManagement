using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.EntityModels
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
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
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

        public int? CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }

       
        [ForeignKey("CreatedByUserID")]
        [InverseProperty("UsersCreated")]
        public User CreatedByUser { get; set; }

     
        [ForeignKey("ModifiedByUserID")]
        [InverseProperty("UsersModified")]
        public User ModifiedByUser { get; set; }

        //[Required]
        //public int BankAccountID { get; set; }
        
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public virtual ICollection<User> UsersCreated { get; set; }
        public virtual ICollection<User> UsersModified { get; set; }
        public virtual ICollection<BankAccount> BankAccountsCreated { get; set; }
        public virtual ICollection<BankAccount> BankAccountsModified { get; set; }
        public virtual ICollection<BankAccountAudit> BankAccountAuditsCreated { get; set; }
    }
}
