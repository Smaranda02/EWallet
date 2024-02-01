using EWallet.Common.Enums;
using EWallet.DataAccess.Enums;
using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Transactions.ViewModel
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public string TrasactionDate { get; set; }

        public decimal TrasactionSum { get; set; }

        public int UserId { get; set; }

        public int? IncomeId { get; set; }

        public int? SpendingId { get; set; }

        public int? PiggyBankId { get; set; }

        public string Username { get; set; }

        public string TransactionTime { get; set; } 

        public string? SpendingCategory { get; set; }


    }
}
