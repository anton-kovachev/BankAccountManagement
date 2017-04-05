using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.DTOModels.Role
{
    public class UserRoleDTO
    {
        public string RoleName { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
