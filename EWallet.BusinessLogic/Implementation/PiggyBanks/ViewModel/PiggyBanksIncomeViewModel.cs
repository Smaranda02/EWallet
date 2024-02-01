using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public class PiggyBanksIncomeViewModel
    {
        public int IncomeId { get; set; }

        public int PiggyBankId { get; set; }

        public decimal AllocatedIncomeAmount { get; set; }

        public string IncomeName { get; set; }
    }
}
