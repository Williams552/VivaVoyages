using System;
using System.Collections.Generic;

namespace VivaVoyages.mol;

public partial class Destination
{
    public int? PlaceId { get; set; }

    public int? TourId { get; set; }

    public string? Description { get; set; }

    public DateOnly? DateVisit { get; set; }

    public virtual Place? Place { get; set; }

    public virtual Tour? Tour { get; set; }
}
