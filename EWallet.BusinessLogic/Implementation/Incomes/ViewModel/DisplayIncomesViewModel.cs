using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes.ViewModel
{
    public class DisplayIncomesViewModel
    {
        public List<IncomeViewModel> IncomeViewModels { get; set; } = new();
        public decimal? IncomeSum { get; set; }

        public string? IncomeDescription { get; set; }

        public List<SelectListItem> RecurrenceTypes { get; set; } = new();

        public List<SelectListItem> PiggyBanks { get; set; } = new();




    }
}
