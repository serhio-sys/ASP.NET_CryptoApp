using CryptoApp.Models;
using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace CryptoApp.Pages
{
    public class RegisterModel : BasePageModel
    {

        public List<string> errors = new List<string>();
        private readonly DatabaseContext databaseContext;

        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex validEmail = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

        public RegisterModel(DatabaseContext context, UserService user) : base (user)
        {
            databaseContext = context;
        }

        public IActionResult OnPost(Models.User user, string Password1)
        {
            bool HasErrors = false;
            CryptoApp.Models.User? user1 = databaseContext.Users.FirstOrDefault(user2 => user2.Username == user.Username || user2.Email == user.Email);
            if (Password1.Length < 6 || !hasNumber.IsMatch(Password1) || !hasUpperChar.IsMatch(Password1))
            {
                errors.Add("Invalid format of password");
                HasErrors = true;
            }
            if (!validEmail.IsMatch(user.Email))
            {
                errors.Add($"Invalid format of email: {user.Email}");
                HasErrors = true;
            }
            if (user1 != null)
            {
                errors.Add($"User with this username or email is already created!");
                HasErrors = true;
            }
            if (Password1 != user.Password)
            {
                errors.Add("Passwords is not equals");
            }
            if (HasErrors)
            {
                return Page();
            }
            user.IsAdmin = false;
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
