using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class VwSpendingCategoriesCount
{
    public int SpendingCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int UserId { get; set; }

    public int? Appearances { get; set; }
}
