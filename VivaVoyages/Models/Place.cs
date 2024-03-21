using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Place
{    [Display(Name = "Place Id")]
    public int PlaceId { get; set; }
 [Display(Name = "Place Name")]
    public string PlaceName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; }

 [Display(Name = "Image Path")]
    public string? ImagePath { get; set; }

    [NotMapped]
    public IFormFile? Image { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();
}
