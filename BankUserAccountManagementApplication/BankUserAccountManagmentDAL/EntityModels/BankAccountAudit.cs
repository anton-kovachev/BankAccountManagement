using BankUserAccountManagmentDAL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.EntityModels
{
    public enum AccountOperation
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3
    }

    public class BankAccountAudit
    {
        public int ID { get; set; }
        public decimal PreviousAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public decimal ChangeAmount { get; set; }

        public string Operation { get; set; }

        public int BankAccountID { get; set; }

        public int BankAccountOperationID { get; set; }

        public int? TransferBankAccountID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int CreatedByUserID { get; set; }

        [Required]
        [ForeignKey("CreatedByUserID")]
        [InverseProperty("BankAccountAuditsCreated")]
        public virtual User CreatedByUser { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual BankAccountOperation BankAccountOperation  { get;set;}

        [Required]
        [ForeignKey("TransferBankAccountID")]
        [InverseProperty("TransferBankAccountAudits")]
        public virtual BankAccount TransferBankAccount { get; set; }
    }
}
