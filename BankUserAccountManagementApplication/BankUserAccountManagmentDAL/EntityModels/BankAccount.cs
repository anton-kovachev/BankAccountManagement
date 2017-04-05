using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.EntityModels
{
    public class BankAccount
    {
        public int ID { get; set; }

        public decimal Amount { get; set; }
        public bool IsActive { get; set; }

        public int UserID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        public int CreatedByUserID { get; set; }
        public int ModifiedByUserID { get; set; }

        [Required]
        [ForeignKey("CreatedByUserID")]
        [InverseProperty("BankAccountsCreated")]
        public virtual User CreatedByUser { get; set; }

        [Required]
        [ForeignKey("ModifiedByUserID")]
        [InverseProperty("BankAccountsModified")]
        public virtual User ModifiedByUser { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BankAccountAudit> BankAccountAudits { get; set; }

        public virtual ICollection<BankAccountAudit> TransferBankAccountAudits { get; set; }
    }
}
