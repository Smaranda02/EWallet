using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class StoredDescription
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? DescriptionText { get; set; }

    public virtual User? User { get; set; }
}
