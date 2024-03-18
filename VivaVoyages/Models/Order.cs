using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int? StaffId { get; set; }

    public int TourId { get; set; }

    public string CouponCode { get; set; }

    public string Status { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? Total { get; set; }

    public virtual Coupon CouponCodeNavigation { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual Staff Staff { get; set; }

    public virtual Tour Tour { get; set; }
}
