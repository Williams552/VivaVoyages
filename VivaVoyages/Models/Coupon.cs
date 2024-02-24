using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Coupon
{
    public string CouponCode { get; set; } = null!;

    public decimal Discount { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateEnd { get; set; }
}
