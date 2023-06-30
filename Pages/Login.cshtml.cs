using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApp.Pages
{
    public class LoginModel : BasePageModel
    {
        public List<string> errors = new List<string>();
        private readonly DatabaseContext databaseContext;
        public LoginModel(DatabaseContext context,UserService userService) : base (userService)
        {
            databaseContext = context;
        }
        public void OnGet()
        {
            if (user != null)
            {
                string? url = Url.Page("Index");
                if (url != null) {
                    Response.Redirect(url);
                }
            }
        }

        public virtual IActionResult OnPost(string Username, string Password)
        {
            CryptoApp.Models.User? user = databaseContext.Users.FirstOrDefault(x => x.Username == Username && x.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetString("Id", user.Id.ToString());
            }
            else
            {
                errors.Add("Username or Password is not valid");
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
