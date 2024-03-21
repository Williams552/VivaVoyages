using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VivaVoyages.Models;

public partial class Coupon
{
    [Display(Name = "Coupon Code")]
    public string CouponCode { get; set; } = null!;

    public decimal Discount { get; set; }

    [Display(Name = "Date Start")]
    public DateOnly DateStart { get; set; }

    [Display(Name = "Date End")]
    public DateOnly DateEnd { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
