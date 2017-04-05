using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentApplicationDAL.Repositories;
using BankUserAccountManagmentDAL.DTOModels.BankAccount;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BankUserAccountManagmentDAL.DTOModels.User;
using BankUserAccountManagmentDAL.CustomExceptions;

namespace BankUserAccountManagmentDAL.Repositories
{
    public class BankAccountRepository : BaseRepository
    {
        public BankAccountRepository(int userID) : base(userID)
        {
        }

        public BankAccountRepository(BaseRepository baseRepository) : base(baseRepository)
        {
        }

        public BankAccountRepository(IBaseRepository baseRepository) : base(baseRepository.Context, baseRepository.UserID)
        {
        }

        public BankAccount CreateInitialBankAccountForUser()
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.Amount = 0;
            bankAccount.CreatedByUserID = this.UserID;
            bankAccount.CreatedDate = DateTime.UtcNow;
            bankAccount.ModifiedByUserID = this.UserID;
            bankAccount.ModifiedDate = DateTime.UtcNow;
            bankAccount.IsActive = true;

            return bankAccount;
        }

        public int GetBankAccountOwnerId(int accountId)
        {
            int accountOwnerUserID = this.Context.BankAccounts.Where(ba => ba.ID == accountId && ba.IsActive).Select(ba => ba.UserID).Single();
            return accountOwnerUserID;
        }

        public BankAccountDTO GetBankAccountDTOById(int accountId)
        {
            BankAccountDTO bankAccountDTO = this.Context.BankAccounts.Where(ba => ba.ID == accountId && ba.IsActive)
                .Select(ba => new BankAccountDTO
                {
                    ID = ba.ID,
                    Amount = ba.Amount,
                    Owner = new UserBasicDTO { ID = ba.UserID, UserName = ba.User.Email },
                    CreatedByUser = new UserBasicDTO { ID = ba.CreatedByUserID, UserName = ba.CreatedByUser.Email },
                    CreatedDate = ba.CreatedDate,
                    ModifiedByUser = new UserBasicDTO { ID = ba.ModifiedByUserID, UserName = ba.ModifiedByUser.Email },
                    ModifiedDate = ba.ModifiedDate,
                    BankAccountAuditList = ba.BankAccountAudits.Select( bau => new BankAccountAuditDTO {
                                                                        ID = bau.ID,
                                                                        PreviousAmmount = bau.PreviousAmount,
                                                                        CurrentAmount = bau.CurrentAmount,
                                                                        ChangeAmount = bau.ChangeAmount,
                                                                        BankAccountOperation = bau.BankAccountOperation.Name,
                                                                        TranserBankAccountID = bau.TransferBankAccountID,
                                                                        CreatedByUser = new UserBasicDTO { ID = bau.CreatedByUserID,
                                                                                                           UserName = bau.CreatedByUser.Email },
                                                                        CreatedDate = bau.CreatedDate
                                                                       }).OrderByDescending( bau => bau.ID ).ToList()
                }).Single();

            return bankAccountDTO;
        }

        public bool DoesBankAccountExists(int accountId)
        {
            bool doesAccountExists = this.Context.BankAccounts.Any(ba => ba.ID == accountId);
            return doesAccountExists;
        }

        public bool DepositAmountIntoBankAccount(int accountId, decimal currentAmount, decimal amountToDeposit, DateTime accountLastModifiedDate)
        {
            using (this.Context.Database.BeginTransaction())
            {
                BankAccount bankAccount = this.Context.BankAccounts.Find(accountId);

                if (!CompareCurrentWithLastKnownDate(accountLastModifiedDate, bankAccount.ModifiedDate))//| bankAccount.Amount != currentAmount)
                {
                    throw new InvalidBankAccountModifiedTimeStamp("The Bank Account has been modified since your last read, state is inconsistent!");
                }

              
                bankAccount.ModifiedDate = DateTime.UtcNow;
                bankAccount.ModifiedByUserID = this.UserID;

                BankAccountAudit bankAccountAudit = new BankAccountAudit();
               
                bankAccountAudit.PreviousAmount = bankAccount.Amount;

                bankAccount.Amount = bankAccount.Amount + amountToDeposit;

                bankAccountAudit.CurrentAmount = bankAccount.Amount;

                bankAccountAudit.ChangeAmount = amountToDeposit;

                bankAccountAudit.CreatedByUserID = this.UserID;
                bankAccountAudit.CreatedDate = bankAccount.ModifiedDate;

                bankAccountAudit.BankAccountOperationID = (int)AccountOperation.Deposit;
                bankAccountAudit.TransferBankAccount = bankAccount;

                bankAccount.BankAccountAudits = new List<BankAccountAudit>();
                bankAccount.BankAccountAudits.Add(bankAccountAudit);

                this.Context.BankAccountAudits.Add(bankAccountAudit);
                //this.Context.Entry(bankAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
              
                this.Context.SaveChanges();

                this.Context.Database.CommitTransaction();
            }

            return true;
        }

        public bool WithdrawAmountIntoBankAccount(int accountId, decimal currentAmount, decimal amountToWithdraw, DateTime accountLastModifiedDate)
        {
            
            using (this.Context.Database.BeginTransaction())
            {
                BankAccount bankAccount = this.Context.BankAccounts.Find(accountId);

                if (!CompareCurrentWithLastKnownDate(accountLastModifiedDate, bankAccount.ModifiedDate))
                {
                    throw new InvalidBankAccountModifiedTimeStamp("The Bank Account has been modified since your last read, state is inconsistent!");
                }

                if(bankAccount.Amount < amountToWithdraw)
                {
                    throw new InvalidBankAccountWithdrawException("The Amount in the Account is less than the amount the user wants to withdraw!");
                }

                bankAccount.ModifiedDate = DateTime.UtcNow;
                bankAccount.ModifiedByUserID = this.UserID;

                BankAccountAudit bankAccountAudit = new BankAccountAudit();

                bankAccountAudit.PreviousAmount = bankAccount.Amount;

                bankAccount.Amount = bankAccount.Amount - amountToWithdraw;

                bankAccountAudit.CurrentAmount = bankAccount.Amount;

                bankAccountAudit.ChangeAmount = -amountToWithdraw;

                bankAccountAudit.CreatedByUserID = this.UserID;
                bankAccountAudit.CreatedDate = bankAccount.ModifiedDate;

                bankAccountAudit.BankAccountOperationID = (int)AccountOperation.Withdraw;
                bankAccountAudit.TransferBankAccount = bankAccount;

                bankAccount.BankAccountAudits = new List<BankAccountAudit>();
                bankAccount.BankAccountAudits.Add(bankAccountAudit);

                this.Context.BankAccountAudits.Add(bankAccountAudit);

                this.Context.SaveChanges();

                this.Context.Database.CommitTransaction();
            }

            return true;
        }

        public bool TransferAmountFromAccountToAccount( int accountIdToWithdraw, int accountIdToDeposit, decimal accountToWithdrawCurrentAmount, decimal amountToTransfer, DateTime accountIdToWithdrawLastModifiedDate)
        {
            using (this.Context.Database.BeginTransaction())
            {
                BankAccount bankAccountToWithdraw = this.Context.BankAccounts.Find(accountIdToWithdraw);
                BankAccount bankAccountToDeposit = this.Context.BankAccounts.Find(accountIdToDeposit);

                if (!CompareCurrentWithLastKnownDate(accountIdToWithdrawLastModifiedDate, bankAccountToWithdraw.ModifiedDate))
                {
                    throw new InvalidBankAccountModifiedTimeStamp("The Bank Account has been modified since your last read, state is inconsistent!");
                }

                if (bankAccountToWithdraw.Amount < amountToTransfer)
                {
                    throw new InvalidBankAccountWithdrawException("The Amount in the Account is less than the amount the user wants to withdraw!");
                }

                bankAccountToWithdraw.ModifiedDate = DateTime.UtcNow;
                bankAccountToWithdraw.ModifiedByUserID = this.UserID;

                bankAccountToDeposit.ModifiedDate = DateTime.UtcNow;
                bankAccountToDeposit.ModifiedByUserID = this.UserID;

                BankAccountAudit bankAccountToWithdrawAudit = new BankAccountAudit();
                BankAccountAudit bankAccountToDepositAudit = new BankAccountAudit();

                bankAccountToWithdrawAudit.PreviousAmount = bankAccountToWithdraw.Amount;
                bankAccountToDepositAudit.PreviousAmount = bankAccountToDeposit.Amount;

                bankAccountToWithdraw.Amount = bankAccountToWithdraw.Amount - amountToTransfer;
                bankAccountToDeposit.Amount = bankAccountToDeposit.Amount + amountToTransfer;

                bankAccountToWithdrawAudit.CurrentAmount = bankAccountToWithdraw.Amount;
                bankAccountToDepositAudit.CurrentAmount = bankAccountToDeposit.Amount;

                bankAccountToWithdrawAudit.ChangeAmount = -amountToTransfer;
                bankAccountToDepositAudit.ChangeAmount = amountToTransfer;

                bankAccountToWithdrawAudit.CreatedByUserID = this.UserID;
                bankAccountToWithdrawAudit.CreatedDate = bankAccountToWithdraw.ModifiedDate;

                bankAccountToDepositAudit.CreatedByUserID = this.UserID;
                bankAccountToDepositAudit.CreatedDate = bankAccountToDeposit.ModifiedDate;

                bankAccountToWithdrawAudit.BankAccountOperationID = (int)AccountOperation.Transfer;
                bankAccountToWithdrawAudit.TransferBankAccount = bankAccountToDeposit;

                bankAccountToDepositAudit.BankAccountOperationID = (int)AccountOperation.Transfer;
                bankAccountToDepositAudit.TransferBankAccount =  bankAccountToWithdraw;

                bankAccountToWithdraw.BankAccountAudits = new List<BankAccountAudit>();
                bankAccountToWithdraw.BankAccountAudits.Add(bankAccountToWithdrawAudit);

                bankAccountToDeposit.BankAccountAudits = new List<BankAccountAudit>();
                bankAccountToDeposit.BankAccountAudits.Add(bankAccountToDepositAudit);

                this.Context.BankAccountAudits.Add(bankAccountToWithdrawAudit);
                this.Context.BankAccountAudits.Add(bankAccountToDepositAudit);

                this.Context.SaveChanges();

                this.Context.Database.CommitTransaction();
            }

            return true;
        }

        private bool CompareCurrentWithLastKnownDate(DateTime accountLastModifiedKnowDate, DateTime accountLastModifiedCurrentDate )
        {
            return (accountLastModifiedKnowDate.Date == accountLastModifiedCurrentDate.Date && accountLastModifiedKnowDate.Hour == accountLastModifiedCurrentDate.Hour &&
                     accountLastModifiedKnowDate.Minute == accountLastModifiedCurrentDate.Minute && accountLastModifiedKnowDate.Second == accountLastModifiedCurrentDate.Second);
        }
    }
}
