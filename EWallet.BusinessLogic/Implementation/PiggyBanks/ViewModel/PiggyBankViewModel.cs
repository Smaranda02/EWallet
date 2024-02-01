using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public class PiggyBankViewModel 
    {
        public int Id { get; set; }

        public decimal TargetSum { get; set; }

        public DateTime? DueDate { get; set; }

        public string PiggyBankDescription { get; set; }

        public int SavingPriority { get; set; }

        public int PiggyBankStatus { get; set; }
        public string PiggyBankStatusName { get; set; }


        public decimal CurrentBalance { get; set; }

        public List<PiggyBanksIncomeViewModel> PiggyBanksIncomes { get; set; } = new();

        public List<FriendViewModel> Collaborators { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

        public int PiggyBankFriendsId { get; set; }

    }
}
