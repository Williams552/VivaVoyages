using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VivaVoyages.Models;

public partial class Customer
{    [Display(Name = "Customer Id")]
    public int CustomerId { get; set; }
 [Display(Name = "Full Name")]
    [Required]
    public string FullName { get; set; } = null!;
 
    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
 [Display(Name = "Phone Number")]
    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Display(Name = "Date of Birth")]
    public DateOnly Dob { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    public string? ResetCode { get; set; }

    
    public string Status { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [Compare("Password")]
    [NotMapped]
    public string ConfirmPassword { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
