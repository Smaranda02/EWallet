using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBanksIncome
{
    public int IncomeId { get; set; }

    public int PiggyBankId { get; set; }

    public decimal AllocatedIncomeAmount { get; set; }

    public virtual Income Income { get; set; } = null!;

    public virtual PiggyBank PiggyBank { get; set; } = null!;
}
