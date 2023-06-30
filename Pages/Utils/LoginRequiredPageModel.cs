using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages.Utils
{
    public class LoginRequiredPageModel : BasePageModel
    {
        public LoginRequiredPageModel(UserService userService) : base (userService) 
        {
        }

        protected bool IsAuthorized()
        {
            string? url = Url.Page("Index");
            if (user == null)
            {
                if (url != null)
                {
                    Response.Redirect(url);
                }
                return false;
            }
            return true;
        }
    }
}
