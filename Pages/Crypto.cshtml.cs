using CryptoApp.Models;
using CryptoApp.Pages.Utils;
using CryptoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoApp.Pages
{
    public class CryptoModel : BasePageModel
    {
        public int Id { get; set; }
        public int page { get; set; }
        public List<Coin>? Coins { get; set; }
        public int countOfPages { get; set; }
        public List<CoinUser> CoinUsers { get; set; }
        public DatabaseContext databaseContext { get; set; }
        public CryptoModel(DatabaseContext context, UserService userService) : base(userService) 
        {
            databaseContext = context;
            CoinUsers = databaseContext.CoinUsers.ToList();
        }

        private void GettingUser()
        {
            int id;
            int.TryParse(HttpContext.Session.GetString("Id"), out id);
            if (id != 0)
            {
                Id = id;
                user = databaseContext.Users.FirstOrDefault(it => it.Id == id);
            }
            else
            {
                Id = 0;
            }
        }

        public void OnGet(int Cpage)
        {
            GettingUser();
            page = Cpage;
            countOfPages = databaseContext.Coins.Count();
            Coins = databaseContext.Coins.Skip(Cpage * 50).Take(50).ToList();
        }
        public void OnPost(string CoinName = "", int SortSelect = 0, bool toUp = false)
        {
            GettingUser();
            List<Coin> coins;
            if (!String.IsNullOrEmpty(CoinName))
            {
                coins = databaseContext.Coins.Where(it => it.Name.ToLower().IndexOf(CoinName.ToLower()) > -1).Take(50).ToList();
            }
            else
            {
                coins = databaseContext.Coins.Take(50).ToList();
            }
            if (toUp == true)
            {
                coins = coins.Where(o => !o.isFall).ToList();
            }
            if (SortSelect == 1)
            {
                coins = coins.OrderBy(o => o.Price).ToList();
            }
            else if (SortSelect == 2)
            {
                coins = coins.OrderBy(o => o.Price).Reverse().ToList();
            }
            Coins = coins;
        }
    }
}
