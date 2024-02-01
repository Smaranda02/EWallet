using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class RecurrenceType
{
    public int Id { get; set; }

    public string RecurrenceTypeName { get; set; } = null!;

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Spending> Spendings { get; set; } = new List<Spending>();
}
