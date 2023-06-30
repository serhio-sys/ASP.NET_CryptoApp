using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly UserService _userService;
        public LogoutModel(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult OnGet()
        {
            HttpContext.Session.SetString("Id","0");
            _userService.user = null;
            return RedirectToPage("Index");
        }
    }
}
