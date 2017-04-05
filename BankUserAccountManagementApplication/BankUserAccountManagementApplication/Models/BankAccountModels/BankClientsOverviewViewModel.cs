using BankUserAccountManagementApplication.Models.UserProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Models
{
    public class BankClientsOverviewViewModel
    {
        [Display(Name ="Number of Clients")]
        public int NumberOfClients { get; set; }
        [Display(Name = "Total Cash Deposit")]
        public decimal TotalAmountOfMoneyDeposit { get; set; }
        public IEnumerable<UserProfileShortAmountViewModel> BankClients { get; set; }
    }
}
