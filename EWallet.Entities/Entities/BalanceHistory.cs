using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class BalanceHistory
{
    public int Id { get; set; }

    public int BalanceYear { get; set; }

    public int BalanceMonth { get; set; }

    public int UserId { get; set; }

    public decimal Spendings { get; set; }

    public decimal Incomes { get; set; }

    public decimal PiggyBanksSum { get; set; }

    public virtual User User { get; set; } = null!;
}
