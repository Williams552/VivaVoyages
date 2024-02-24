using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Consist
{
    public string CouponCode { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Coupon CouponCodeNavigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
