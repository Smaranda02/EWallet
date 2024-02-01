using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public class DisplayPiggyBanksViewModel 
    {
        public int CurrentUserId { get; set; }
        public List<PiggyBankViewModel> PersonalPiggyBanks { get; set; } = new();

        public List<PiggyBankViewModel> CollaborativePiggyBanks { get; set; } = new();

        public List<SelectListItem> Incomes { get; set; } = new();

        public List<EditCollaborativePiggyBankViewModel> EditCollaborativePiggyBankViewModels { get; set; }

    }
}
