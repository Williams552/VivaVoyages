using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VivaVoyages.Models;

public partial class Tour
{
   [Display(Name = "Customer Id")]
   public int TourId { get; set; }

   [DisplayName("Tour Name")]
   public string TourName { get; set; } = null!;

   [DisplayName("Expected Profit")]
   public decimal ExpectedProfit { get; set; }

   [DisplayName("Date Start")]
   public DateOnly DateStart { get; set; }

   [DisplayName("Tour Dates")]
   public int TourDates { get; set; }

   [DisplayName("Max Passeger")]
   public int MaxPasseger { get; set; }

   [DisplayName("Tour Guide")]
   public string TourGuide { get; set; } = null!;

   public decimal Cost { get; set; }

   public decimal? Tax { get; set; }

   [DisplayName("Single Room Cost")]
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