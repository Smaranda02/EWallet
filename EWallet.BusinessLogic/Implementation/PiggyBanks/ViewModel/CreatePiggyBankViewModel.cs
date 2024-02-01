﻿using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel
{
    public  class CreatePiggyBankViewModel
    {
        public decimal TargetSum { get; set; }

        public DateTime? DueDate { get; set; }

        public string PiggyBankDescription { get; set; }

        public int SavingPriority { get; set; }

        public int? PiggyBankStatus { get; set; }

        public List<int> IncomeIds { get; set; }

        public  List<PiggyBanksIncomeViewModel> PiggyBanksIncomes { get; set; } = new();


    }
}
