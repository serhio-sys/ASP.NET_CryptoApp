using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages
{
    public class HomePageModel : BasePageModel
    {
        public HomePageModel(UserService userService) : base(userService) { }
    
    }
}
