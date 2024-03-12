using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace VivaVoyages.Models
{
    public class LoginChecker
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginChecker(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CheckStaff()
        {
            var staffCheck = _httpContextAccessor.HttpContext.Session.GetString("LoggedInStaff");
            
            if (staffCheck == null)
            {
                return new RedirectToActionResult("StaffLogin", "LoginRegister", null);
            }

            return null;
        }
    }
}