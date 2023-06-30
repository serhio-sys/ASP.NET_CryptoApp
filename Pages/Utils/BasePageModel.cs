using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages.Utils
{
    public class BasePageModel : PageModel
    {
        public User? user { get; set; } = null;
        public BasePageModel (UserService userService)
        {
            user = userService.user;
        }
    }
}
