using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Destination
{
    [Display(Name = "Destination Id")]
    public int DestinationId { get; set; }
     [Display(Name = "Place Id")]
    public int? PlaceId { get; set; }
 [Display(Name = "Tour Id")]
    public int? TourId { get; set; }

    public string? Description { get; set; }
     [Display(Name = "Date Visit")]
    public DateOnly? DateVisit { get; set; }

    public virtual Place? Place { get; set; }

    public virtual Tour? Tour { get; set; }
}
