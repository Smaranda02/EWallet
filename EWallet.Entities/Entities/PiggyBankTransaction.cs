using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBankTransaction
{
    public int Id { get; set; }

    public DateTime? PiggyBankDateTime { get; set; }

    public int? PiggyBankId { get; set; }

    public decimal? PiggyBankSum { get; set; }

    public int? IncomeId { get; set; }

    public virtual Income? Income { get; set; }

    public virtual PiggyBank? PiggyBank { get; set; }
}
