using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int customerId { get; set; }

    public int TourId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
