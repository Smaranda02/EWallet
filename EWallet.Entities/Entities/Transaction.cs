using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public DateTime TrasactionDateTime { get; set; }

    public decimal TrasactionSum { get; set; }

    public int UserId { get; set; }

    public int? IncomeId { get; set; }

    public int? SpendingId { get; set; }

    public int? PiggyBankId { get; set; }

    public virtual ICollection<ImmediateTransaction> ImmediateTransactions { get; set; } = new List<ImmediateTransaction>();

    public virtual Income? Income { get; set; }

    public virtual PiggyBank? PiggyBank { get; set; }

    public virtual Spending? Spending { get; set; }

    public virtual User User { get; set; } = null!;
}
