﻿using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Destination
{
    public int PlaceId { get; set; }

    public int TourId { get; set; }

    public string? Description { get; set; }

    public DateOnly? DateVisit { get; set; }

    public virtual Place Place { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
