using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string? ImageName { get; set; }

    public byte[] Photo { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Spending IdNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
