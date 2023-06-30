using CryptoApp.Models;
using CryptoApp.Pages.Utils;
using CryptoApp.Services;

namespace CryptoApp.Pages
{
    public class FollowedModel : LoginRequiredPageModel
    {
        public List<Coin>? Coins { get; set; }
        public readonly DatabaseContext db;
        public FollowedModel(UserService userService,DatabaseContext context) : base(userService)
        {
            db = context;
        }

        public void OnGet()
        {
            bool isAuthenticated = IsAuthorized();
            if (isAuthenticated) { 
                List<Guid> guids = new List<Guid>();
                foreach (CoinUser item in db.CoinUsers.ToList())
                {
                    if (item.UserId == user?.Id)
                    {
                        guids.Add(item.CoinId);
                    }
                }
                Coins = db.Coins.Where(it => guids.Contains(it.Id)).ToList();
            }
        }

    }
}
