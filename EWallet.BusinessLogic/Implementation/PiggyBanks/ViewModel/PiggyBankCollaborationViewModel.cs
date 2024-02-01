using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public class PiggyBankCollaborationViewModel
    {
        public int Id { get; set; }
        public int PiggyBankId { get; set; }

        public int UserId { get; set; }
    }
}
