using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Order
{    [Display(Name = "Order Id")]
    public int OrderId { get; set; }
 [Display(Name = "Customer Id")]
    public int CustomerId { get; set; }
 [Display(Name = "Staff Id")]
    public int? StaffId { get; set; }
 [Display(Name = "Tour Id")]
    public int TourId { get; set; }
 [Display(Name = "Coupon code")]
    public string? CouponCode { get; set; }

    public string Status { get; set; } = null!;
 [Display(Name = "Date Created")]
    public DateTime DateCreated { get; set; }

    public decimal? Total { get; set; }

    public virtual Coupon? CouponCodeNavigation { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual Staff? Staff { get; set; }

    public virtual Tour Tour { get; set; } = null!;
}
