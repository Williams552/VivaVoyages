using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VivaVoyages.Models;

public partial class Tour
{
     [Display(Name = "Customer Id")]
    public int TourId { get; set; }

     [Display(Name = "Tour Name")]

    public string TourName { get; set; } = null!;

 [Display(Name = "Expected Profit")]
    public decimal ExpectedProfit { get; set; }

     [Display(Name = "Date Start")]

    public DateOnly DateStart { get; set; }

     [Display(Name = "Tour Dates")]

    public int TourDates { get; set; }
 [Display(Name = "Max Passeger")]
    public int MaxPasseger { get; set; }

 [Display(Name = "Tour Guide")]
    public string TourGuide { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal? Tax { get; set; }

     [Display(Name = "Single Room Cost")]

    public decimal? SingleRoomCost { get; set; }

     [Display(Name = "Image Path")]
    public string? ImagePath { get; set; }

 [Display(Name = "Image File")]
    [NotMapped]
    public IFormFile ImageFile { get; set; }
     [Display(Name = "Coupon code")]
    public string? CouponCode { get; set; }

    public virtual Coupon? CouponCodeNavigation { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}