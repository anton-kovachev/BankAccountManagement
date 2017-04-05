using BankUserAccountManagementApplication.Models.BankAccountModels;
using BankUserAccountManagementApplication.Models.UserProfileModels;
using BankUserAccountManagmentDAL.DTOModels.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Helpers
{
    public static class BankAccountMapHelpers
    {
        public static BankAccountViewModel MapBankAccountDTOToViewModel(BankAccountDTO bankAccountDTO)
        {
            BankAccountViewModel bankAccountViewModel = new BankAccountViewModel
            {
                ID = bankAccountDTO.ID,
                Amount = bankAccountDTO.Amount,
                Owner = new UserProfileBasicViewModel
                {
                    ID = bankAccountDTO.Owner.ID,
                    UserName = bankAccountDTO.Owner.UserName,
                    Email = bankAccountDTO.Owner.UserName
                },
                CreatedByUser = new UserProfileBasicViewModel
                {
                    ID = bankAccountDTO.CreatedByUser.ID,
                    UserName = bankAccountDTO.CreatedByUser.UserName,
                    Email = bankAccountDTO.CreatedByUser.UserName
                },
                CreatedDate = bankAccountDTO.CreatedDate,
                ModifiedByUser = new UserProfileBasicViewModel
                {
                    ID = bankAccountDTO.ModifiedByUser.ID,
                    UserName = bankAccountDTO.ModifiedByUser.UserName,
                    Email = bankAccountDTO.ModifiedByUser.UserName
                },
                ModifiedDate = bankAccountDTO.ModifiedDate,
                BankAccountAuditList = bankAccountDTO.BankAccountAuditList.Select(bau => new BankAccountAuditViewModel
                {
                    ID = bau.ID,
                    CurrentAmount = bau.CurrentAmount,
                    PreviousAmount = bau.PreviousAmmount,
                    ChangeAmount = bau.ChangeAmount,
                    BankAccountOperation = bau.BankAccountOperation,
                    TranserBankAccountID = bau.TranserBankAccountID,
                    CreatedByUser = new UserProfileBasicViewModel
                    {
                        ID = bau.CreatedByUser.ID,
                        UserName = bau.CreatedByUser.UserName,
                        Email = bau.CreatedByUser.UserName
                    },
                    CreatedDate = bau.CreatedDate
                })
            };

            return bankAccountViewModel;
        }

        public static BankAccountDepositViewModel MapBankAccountDTOToDepositViewModel(BankAccountDTO bankAccountDTO)
        {
            BankAccountDepositViewModel bankAccountDepositViewModel = new BankAccountDepositViewModel
            {
                ID = bankAccountDTO.ID,
                AmountCurrent = bankAccountDTO.Amount,
                Owner = bankAccountDTO.Owner.UserName,
                AmountToDeposit = 0,
                ModifiedDate = bankAccountDTO.ModifiedDate
            };

            return bankAccountDepositViewModel;
        }

        public static BankAccountWithdrawViewModel MapBankAccountDTOToWithdrawViewModel(BankAccountDTO bankAccountDTO)
        {
            BankAccountWithdrawViewModel bankAccounToWithdrawViewModel = new BankAccountWithdrawViewModel
            {
                ID = bankAccountDTO.ID,
                AmountCurrent = bankAccountDTO.Amount,
                Owner = bankAccountDTO.Owner.UserName,
                AmountToWithdraw = 0,
                ModifiedDate = bankAccountDTO.ModifiedDate
            };

            return bankAccounToWithdrawViewModel;
        }

        public static BankAccountTransferViewModel MapBankAccountDTOToTransferViewModel(BankAccountDTO bankAccountDTO)
        {
            BankAccountTransferViewModel bankAccountToTransferViewModel = new BankAccountTransferViewModel
            {
                ID = bankAccountDTO.ID,
                AmountCurrent = bankAccountDTO.Amount,
                Owner = bankAccountDTO.Owner.UserName,
                AmountToTransfer = 0,
                BankAccountIDToTransfer = null,
                ModifiedDate = bankAccountDTO.ModifiedDate
            };

            return bankAccountToTransferViewModel;
        }
    }
}
