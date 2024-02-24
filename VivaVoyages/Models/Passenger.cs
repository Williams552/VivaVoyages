﻿using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Passenger
{
    public int OrderId { get; set; }

    public string FullName { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public bool SingleRoom { get; set; }

    public virtual Order Order { get; set; } = null!;
}