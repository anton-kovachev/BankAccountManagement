using BankUserAccountManagmentApplicationDAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.EntityModels
{
    public class BankAccountOperation
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BankAccountAudit> BankAccountAudits { get; set; }
    }
}
