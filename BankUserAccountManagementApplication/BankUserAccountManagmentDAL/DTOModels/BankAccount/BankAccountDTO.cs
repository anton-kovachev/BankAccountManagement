using BankUserAccountManagmentDAL.DTOModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.DTOModels.BankAccount
{
    public class BankAccountDTO
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }

        public UserBasicDTO Owner { get; set; }
        public UserBasicDTO CreatedByUser { get; set; }
        public UserBasicDTO ModifiedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public IEnumerable<BankAccountAuditDTO> BankAccountAuditList { get; set; }
    }
}
