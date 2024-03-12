using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    public string FullName { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Password { get; set; }

    public string? ResetCode { get; set; }

    public string Status { get; set; }

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

    public virtual ICollection<Passenger>? Passengers { get; set; } = new List<Passenger>();
}
