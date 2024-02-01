using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Spending
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public int SpendingCategoryId { get; set; }

    public string SpendingDescription { get; set; } = null!;

    public int UserId { get; set; }

    public int? RecurringNumber { get; set; }

    public int? RecurrenceTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Image? Image { get; set; }

    public virtual ICollection<ImmediateTransaction> ImmediateTransactions { get; set; } = new List<ImmediateTransaction>();

    public virtual RecurrenceType? RecurrenceType { get; set; }

    public virtual SpendingCategory SpendingCategory { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
