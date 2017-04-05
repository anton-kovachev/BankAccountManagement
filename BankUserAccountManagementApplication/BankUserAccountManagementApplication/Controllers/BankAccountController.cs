using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankUserAccountManagmentApplicationDAL.Repositories;
using Microsoft.AspNetCore.Http;
using BankUserAccountManagmentDAL.Repositories;
using BankUserAccountManagementApplication.Helpers;
using Microsoft.AspNetCore.Authorization;
using BankUserAccountManagementApplication.Models.BankAccountModels;
using BankUserAccountManagmentDAL.Constants;
using BankUserAccountManagmentDAL.CustomExceptions;

namespace BankUserAccountManagementApplication.Controllers
{
    [Authorize]
    public class BankAccountController : BaseController 
    {
        private BankAccountRepository bankAccountRepository;
        public BankAccountController(IBaseRepository baseRepository, IHttpContextAccessor httpContextAccessor) : base(baseRepository, httpContextAccessor)
        {
            bankAccountRepository = new BankAccountRepository(baseRepository);
        }

        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Index(int id)
        {
            var bankAccountDTO = bankAccountRepository.GetBankAccountDTOById(id);
            var bankAccountViewModel = BankAccountMapHelpers.MapBankAccountDTOToViewModel(bankAccountDTO);

            return View(bankAccountViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Deposit(int id)
        {
            var bankAccountDTO = bankAccountRepository.GetBankAccountDTOById(id);
            BankAccountDepositViewModel bankAccountDepositViewModel = BankAccountMapHelpers.MapBankAccountDTOToDepositViewModel(bankAccountDTO);

            return View(bankAccountDepositViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Deposit(int id, BankAccountDepositViewModel bankAccountDepositViewModel)
        {
            if (ModelState.IsValid) {
                try
                {
                    bool depositOperationResult =
                        bankAccountRepository.DepositAmountIntoBankAccount(id, bankAccountDepositViewModel.AmountCurrent, bankAccountDepositViewModel.AmountToDeposit, bankAccountDepositViewModel.ModifiedDate);

                    if (depositOperationResult)
                    {
                        return RedirectToAction("Index", "BankAccount", new { @id = id });
                    }
                }
                catch(InvalidBankAccountModifiedTimeStamp ex)
                {
                    //ViewData["AmountToDepositError"] = "The Amount of the Bank account has been modified Please, refresh the page and try to deposit again!";
                    return RedirectToAction("Deposit", "BankAccount", new { @id = id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Something wen wrong! Please retry the operation!");
                }
           
            }

            return View(bankAccountDepositViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Withdraw(int id)
        {
            var bankAccountDTO = bankAccountRepository.GetBankAccountDTOById(id);
            BankAccountWithdrawViewModel bankAccounToWithdrawViewModel = BankAccountMapHelpers.MapBankAccountDTOToWithdrawViewModel(bankAccountDTO);

            return View(bankAccounToWithdrawViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Withdraw(int id, BankAccountWithdrawViewModel bankAccountWithdrawViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool withdrawOperationResult =
                        bankAccountRepository.WithdrawAmountIntoBankAccount(id, bankAccountWithdrawViewModel.AmountCurrent,
                                                                            bankAccountWithdrawViewModel.AmountToWithdraw, bankAccountWithdrawViewModel.ModifiedDate);

                    if (withdrawOperationResult)
                    {
                        return RedirectToAction("Index", "BankAccount", new { @id = id });
                    }
                }
                catch (InvalidBankAccountModifiedTimeStamp ex)
                {
                    //ViewData["AmountToWithdrawError"] = "The Amount of the Bank account has been modified Please, refresh the page and try to deposit again!";
                    return RedirectToAction("Withdraw", "BankAccount", new { @id = id });
                }
                catch(InvalidBankAccountWithdrawException ex)
                {
                    ModelState.AddModelError("AmountToWithdraw", "The Amount of money to withdraw exceeds the Amount of money in the Bank Account!");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Something wen wrong! Please retry the operation!");
                }
            }

            return View(bankAccountWithdrawViewModel);
        }


        [HttpGet]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Transfer(int id)
        {
            var bankAccountDTO = bankAccountRepository.GetBankAccountDTOById(id);
            BankAccountTransferViewModel bankAccountToTransferViewModel = BankAccountMapHelpers.MapBankAccountDTOToTransferViewModel(bankAccountDTO);

            return View(bankAccountToTransferViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "BankAccountOperations")]
        public IActionResult Transfer(int id, BankAccountTransferViewModel bankAccountTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                if (bankAccountTransferViewModel.BankAccountIDToTransfer.HasValue &&
                    bankAccountRepository.DoesBankAccountExists(bankAccountTransferViewModel.BankAccountIDToTransfer.Value))
                {
                    try
                    {
                        bool withdrawOperationResult =
                            bankAccountRepository.TransferAmountFromAccountToAccount(id, bankAccountTransferViewModel.BankAccountIDToTransfer.Value,
                                                                        bankAccountTransferViewModel.AmountCurrent, bankAccountTransferViewModel.AmountToTransfer, bankAccountTransferViewModel.ModifiedDate);

                        if (withdrawOperationResult)
                        {
                            return RedirectToAction("Index", "BankAccount", new { @id = id });
                        }
                    }
                    catch (InvalidBankAccountModifiedTimeStamp ex)
                    {
                        //ViewData["AmountToTransferError"] = "The Amount of the Bank account has been modified Please, refresh the page and try to deposit again!";
                        return RedirectToAction("Transfer", "BankAccount", new { @id = id });
                    }
                    catch (InvalidBankAccountWithdrawException ex)
                    {
                        ModelState.AddModelError("AmountToWithdraw", "The Amount of money to withdraw exceeds the Amount of money in the Bank Account!");
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Something wen wrong! Please retry the operation!");
                    }
                }
                else
                {
                    ModelState.AddModelError("BankAccountIDToTransfer", "The target Account ID does not exists!");
                }
            }

            return View(bankAccountTransferViewModel);
        }

    }
}