﻿using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string TourName { get; set; }

    public string ExpectedProfit { get; set; }

    public DateOnly DateStart { get; set; }

    public int TourDates { get; set; }

    public int MaxPasseger { get; set; }

    public string TourGuide { get; set; }

    public decimal Cost { get; set; }

    public decimal? Tax { get; set; }

    public string ImagePath { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
