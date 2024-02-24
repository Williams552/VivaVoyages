﻿using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string Destination { get; set; } = null!;

    public DateOnly DateStart { get; set; }

    public string TourDates { get; set; } = null!;

    public int MaxPasseger { get; set; }

    public string TourGuide { get; set; } = null!;

    public decimal Cost { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}