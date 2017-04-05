using BankUserAccountManagmentApplicationDAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUserAccountManagmentApplicationDAL.Repositories
{
    public interface IBaseRepository
    {
         BankUserAccountManagementContext Context { get;  }
         int UserID { get; set; }
    }
}
