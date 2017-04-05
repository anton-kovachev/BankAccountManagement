using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.CustomExceptions
{
    public class InvalidBankAccountModifiedTimeStamp : Exception
    {
        public InvalidBankAccountModifiedTimeStamp(string message) : base(message)
        {

        }
    }
}
