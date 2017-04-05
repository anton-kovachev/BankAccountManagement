using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentDAL.DTOModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.DTOModels.BankAccount
{
    public class BankAccountAuditDTO
    {
        public int ID {get; set;}
        public decimal PreviousAmmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public UserBasicDTO CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BankAccountOperation { get; set; }
        public int? TranserBankAccountID { get; set; }
    }
}
