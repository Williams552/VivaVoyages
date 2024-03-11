using System;
using System.Collections.Generic;

namespace VivaVoyages.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Password { get; set; } = null!;

    public string? ResetCode { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
