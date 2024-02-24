using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
    [NotMapped]
    public string confirmPassword { get; set; } = null!;

    public string Status { get; set; } = null!;
}
