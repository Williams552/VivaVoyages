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
            // Check if staff is logged in
            if (!_httpContextAccessor.HttpContext.Session.IsTLoggedIn<Staff>("LoggedInStaff"))
            {
                return new RedirectToActionResult("StaffLogin", "LoginRegister", null);
            }
            return null;

        }
        public IActionResult CheckAdmin()
        {   // Check if the the Staff account is logged in or not first
            if (_httpContextAccessor.HttpContext.Session.IsTLoggedIn<Staff>("LoggedInStaff"))
            {
                // Set varriable to see if it is admin or not
                var role = _httpContextAccessor.HttpContext.Session.GetObject<Staff>("LoggedInStaff").Role;
                // Check if its is admin or not
                if (role == "Administrator")
                {
                    // If yes, dont do anything
                    return null;
                }
                return new RedirectToActionResult("StaffLogin", "LoginRegister", null);
            }
            return new RedirectToActionResult("StaffLogin", "LoginRegister", null);

        }
        public IActionResult CheckCustomer()
        {
            // Check if staff is logged in
            if (!_httpContextAccessor.HttpContext.Session.IsTLoggedIn<Customer>("LoggedInCustomer"))
            {
                return new RedirectToActionResult("Login", "LoginRegister", null);
            }
            return null;

        }
    }
}