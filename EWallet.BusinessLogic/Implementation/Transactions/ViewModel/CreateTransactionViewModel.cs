using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Transactions.ViewModel
{
    public class CreateTransactionViewModel
    {
        public DateTime TrasactionDateTime { get; set; }

        public decimal TrasactionSum { get; set; }

        public int UserId { get; set; }

        public int? IncomeId { get; set; }

        public int? SpendingId { get; set; }

        public int? PiggyBankId { get; set; }

    }
}
