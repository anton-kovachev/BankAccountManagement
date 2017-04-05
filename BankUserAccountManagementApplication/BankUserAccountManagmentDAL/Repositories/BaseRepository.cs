using BankUserAccountManagmentApplicationDAL.Data;
using BankUserAccountManagmentDAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.Repositories
{

    public class BaseRepository : IBaseRepository
    {
        private BankUserAccountManagementContext context = null;
        private int userID;

        public BaseRepository()
        {
            context = new BankUserAccountManagementContext();
            this.userID = (int)UserEnum.SystemUser;
        }

        public BaseRepository(int userID)
        {
            context = new BankUserAccountManagementContext();
            this.userID = userID;
        }
        public BaseRepository(BankUserAccountManagementContext bankUserAccountManagementContext, int userID)
        {
            this.context = bankUserAccountManagementContext;
            this.userID = userID;
        }

        public BaseRepository(BaseRepository baseRepository)
        {
            this.context = baseRepository.context;
            this.userID = baseRepository.userID;
        }

        //public BaseRepository(IBaseRepository baseRepository)
        //{
        //    this.context = baseRepository.Context;
        //    this.userID = baseRepository.UserID;
        //}

        public BankUserAccountManagementContext Context
        {
            get { return this.context; }
        }

        public int UserID
        {
            set { this.userID = value;  }
            get { return this.userID; }
        }
    }
}
