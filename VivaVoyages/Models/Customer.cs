using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivaVoyages.Models;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Address { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(50),MinLength(10)]
    [Display(Name = "Phone Number")]
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    [Display(Name = "Date of Birth")]
    public DateOnly Dob { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [Compare("Password")]
    [NotMapped]
    public string ConfirmPassword { get; set; } = null!;

    public string? ResetCode { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
