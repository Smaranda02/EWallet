using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public class EditCollaborativePiggyBankViewModel
    {
        public int Id { get; set; }
        public List<PiggyBanksIncomeViewModel> CollaborativePiggyBankIncomes { get; set; } = new();

    }
}
