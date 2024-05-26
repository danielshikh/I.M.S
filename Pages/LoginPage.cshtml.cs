using System.Linq;
using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace InventorySystem.Pages
{
	public class LoginPageModel : PageModel
	{
		private readonly InventoryDbContext inventoryDbContext;

		[BindProperty]
		public User UserLogin { get; set; }

		public LoginPageModel(InventoryDbContext inventoryDbContext)
		{
			this.inventoryDbContext = inventoryDbContext;
		}

		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			// Check if the user with the given credentials exists in the database
			var user = inventoryDbContext.User
				.FirstOrDefault(u => u.UserName == UserLogin.UserName && u.Password == UserLogin.Password);

			if (user != null)
			{
				HttpContext.Session.SetString("LoggedInUserName", user.UserName);
				if (!HttpContext.Session.GetString("LoggedInUserName").Equals("Admin"))
					HttpContext.Session.SetString("LoggedInLocation", inventoryDbContext.Branch.Find(user.BranchId).Name); 

                return RedirectToPage("/MainPage");
			}
			else
			{
               // show an error message
                ViewData["Message"] = "Invalid Username or Password";

				return Page();
			}
		}

		public IActionResult OnPostLogout()
		{
			HttpContext.Session.Remove("LoggedInUserName");
			HttpContext.Session.Remove("LoggedInLocation");
            return RedirectToPage("/Index");
        }
    }
}
