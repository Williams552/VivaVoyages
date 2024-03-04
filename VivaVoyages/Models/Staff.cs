using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FullName { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public string Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
