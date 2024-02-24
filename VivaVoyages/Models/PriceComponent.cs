using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class PriceComponent
{
    public int TourId { get; set; }

    public decimal Tax { get; set; }

    public decimal ExpectedProfit { get; set; }

    public virtual Tour Tour { get; set; } = null!;
}
