using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VivaVoyages.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    [Required(ErrorMessage = "Please enter your full name")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter your age")]
    public int? Age { get; set; }

    [Required]
    public string? Gender { get; set; }

    public bool SingleRoom { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
