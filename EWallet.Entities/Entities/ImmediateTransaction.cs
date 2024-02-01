using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class ImmediateTransaction
{
    public int Id { get; set; }

    public DateTime ImmediateTrasactionDateTime { get; set; }

    public decimal ImmediateTrasactionSum { get; set; }

    public int? UserId { get; set; }

    public int? IncomeId { get; set; }

    public int? SpendingId { get; set; }

    public int? TransactionId { get; set; }

    public virtual Income? Income { get; set; }

    public virtual Spending? Spending { get; set; }

    public virtual Transaction? Transaction { get; set; }

    public virtual User? User { get; set; }
}
