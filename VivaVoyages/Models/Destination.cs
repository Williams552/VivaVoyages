using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Destination
{
    public int DestinationId { get; set; }

    public int? PlaceId { get; set; }

    public int? TourId { get; set; }

    public string Description { get; set; }

    public DateOnly? DateVisit { get; set; }

    public virtual Place Place { get; set; }

    public virtual Tour Tour { get; set; }
}
