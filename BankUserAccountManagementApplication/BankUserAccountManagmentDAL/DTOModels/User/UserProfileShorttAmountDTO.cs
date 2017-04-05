using BankUserAccountManagmentApplicationDAL.DTOModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.DTOModels.User
{
    public class UserProfileShortAmountDTO : UserProfileShortDTO
    {
        public decimal BankAccountAmount { get; set; }
    }
}
