using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class ForgotPassword
    {
        public string Email { get; set; }
        public string ResetCode { get; set; }
        public string NewPassword { get; set; }
    }
}