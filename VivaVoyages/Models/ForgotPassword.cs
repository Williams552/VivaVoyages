using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class ForgotPassword
    {
    
        public string Email { get; set; }
        
        [Display(Name = "Reset Code")]
        public string ResetCode { get; set; }

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}