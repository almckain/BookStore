using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Name { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Simulating the database check
            bool isAuthenticated = AuthenticateUser(Name, Email);

            if (isAuthenticated)
            {
                int customerID = 1;
                HttpContext.Session.SetString("CustomerName", Name);
                HttpContext.Session.SetString("CustomerEmail", Email);
                HttpContext.Session.SetInt32("CustomerID", customerID);

                return RedirectToPage("/Customer");
            }

            ModelState.AddModelError(string.Empty, "Inavlid login");
            return Page();
        }

        private bool AuthenticateUser(string name, string email)
        {
            //Simulates the call to the database and would eventually check against the database
            //Returns true by default
            return true;
        }

        public void OnGet()
        {
        }
    }
}
