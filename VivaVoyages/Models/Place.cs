using System;
using System.Collections.Generic;

namespace VivaVoyages.mol;

public partial class Place
{
    public int PlaceId { get; set; }

    public int TourId { get; set; }

    public string PlaceName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DateVisit { get; set; }

    public virtual Tour Tour { get; set; } = null!;
}
