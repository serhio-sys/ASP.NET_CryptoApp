using CryptoApp.Models;
using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages
{
    public class FollowModel : LoginRequiredPageModel

    {
        List<CoinUser> CoinUsers { get; set; }
        private readonly DatabaseContext db;
        public FollowModel(UserService userService,DatabaseContext db) : base(userService)
        {
            this.db = db;
            this.CoinUsers = db.CoinUsers.ToList();
        }
        public void OnGet(string pk,int pg = 1)
        {
            bool isAuthenticated = IsAuthorized();
            if (isAuthenticated)
            {
                Guid guid = Guid.Parse(pk);
                Coin? coin = db.Coins.FirstOrDefault(coin => coin.Id == guid);
                if (coin != null)
                {
                    CoinUser? coinUser = CoinUsers.FirstOrDefault(it => it.UserId == user?.Id && it.CoinId == coin.Id);
                    if (coinUser != null)
                    {
                        db.CoinUsers.Remove(coinUser);
                    }
                    else
                    {
                        coinUser = new CoinUser();
                        coinUser.UserId = (int)(user?.Id);
                        coinUser.CoinId = coin.Id;
                        db.CoinUsers.Add(coinUser);
                    }
                    db.SaveChanges();
                    string? url_crypto = Url.Page("Crypto"), url_followed = Url.Page("Followed");
                    if (url_crypto != null && url_followed != null)
                    {
                        Response.Redirect(pg == 1 ? url_crypto : url_followed);
                    }
                }
            }
        }
    }
}
