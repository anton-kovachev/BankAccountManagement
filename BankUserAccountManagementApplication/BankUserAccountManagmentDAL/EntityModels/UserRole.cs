using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.EntityModels
{
    public class UserRole
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}
