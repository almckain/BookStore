using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebsite.Pages
{
	public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        private readonly ICustomerService _customerService;

        public RegisterModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            bool isSuccess = _customerService.CreateNewCustomer(Name, Email);
            if (isSuccess)
            {
                return RedirectToPage("/Confirmation");
            }
            else
            {
                return Page();
            }
        }
    }
}
