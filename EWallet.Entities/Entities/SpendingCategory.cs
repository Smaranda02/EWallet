using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class SpendingCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<Spending> Spendings { get; set; } = new List<Spending>();
}
