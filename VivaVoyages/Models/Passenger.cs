using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VivaVoyages.Models;

public partial class Passenger

{
    [Display(Name = "Passenger Id")]
    public int PassengerId { get; set; }
    [Display(Name = "Customer Id")]
    public int CustomerId { get; set; }
    [Display(Name = "Order Id")]
    public int OrderId { get; set; }
    [Display(Name = "Full Name")]

    [Required(ErrorMessage = "Please enter your full name")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter your age")]
    public int? Age { get; set; }

    [Required]
    public string? Gender { get; set; }
    [Display(Name = "Single Room")]
    public bool SingleRoom { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
