using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Place
{
    public int PlaceId { get; set; }

    public string PlaceName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Destination>? Destinations { get; set; } = new List<Destination>();
}
