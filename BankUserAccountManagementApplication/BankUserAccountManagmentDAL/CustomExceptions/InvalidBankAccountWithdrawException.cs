using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentDAL.CustomExceptions
{
    public class InvalidBankAccountWithdrawException : Exception
    {
        public InvalidBankAccountWithdrawException(string message) : base(message)
        {

        }
    }
}
