using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace VivaVoyages.Models
{
    public class Customer
    {
        public int customerID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateOnly dateOfBrith { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [NotMapped]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
    }
}
