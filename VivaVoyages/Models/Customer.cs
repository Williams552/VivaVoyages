using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace VivaVoyages.Models
{
    public class Customer
    {
        [Key]
        public int customerID { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full Name must be between 5 and 50 characters.")]
        public String fullName { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Remote("CheckDateOfBirth", "Customer", HttpMethod = "POST", ErrorMessage = "Date of Birth must be at least 18 years ago.")]
        public DateOnly dateOfBrith { get; set; }
        public String phoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters.")]
        public String address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public String email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
        public String password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("password", ErrorMessage = "Password and Confirm Password must match.")]
        [NotMapped] // Not creating a column in the database for Confirm Password
        public string confirmPassword { get; set; }

    }
}