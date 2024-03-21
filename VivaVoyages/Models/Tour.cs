using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace VivaVoyages.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public decimal ExpectedProfit { get; set; }

    public DateOnly DateStart { get; set; }

    public int TourDates { get; set; }

    public int MaxPasseger { get; set; }

    public string TourGuide { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal? Tax { get; set; }

    public decimal? SingleRoomCost { get; set; }

    public string? ImagePath { get; set; }
    
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public string? CouponCode { get; set; }

    public virtual Coupon? CouponCodeNavigation { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
