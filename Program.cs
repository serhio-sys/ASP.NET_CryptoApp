using CryptoApp;
using CryptoApp.Middalwares;
using CryptoApp.Models;
using CryptoApp.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


#pragma warning disable CS8602 // –азыменование веро€тной пустой ссылки.
static async void RefreshingCoins(WebApplicationBuilder builder)
{
    DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    using (DatabaseContext db = new DatabaseContext(options.Options))
    {
        while (true)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://coinranking1.p.rapidapi.com/coins?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h&tiers%5B0%5D=1&orderBy=marketCap&orderDirection=desc&limit=500&offset=0"),
                Headers =
                {
                    { "User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Mobile Safari/537.36" },
                    { "X-RapidAPI-Key", "27d633f222msh2903c78e8ce2968p14edc8jsn684b871ab638" },
                    { "X-RapidAPI-Host", "coinranking1.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                string body = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(body))
                {
                    JObject jsonreaded = (JObject)JToken.Parse(body);
                    Console.WriteLine(jsonreaded["data"]["coins"]);
                    JArray? array = jsonreaded["data"]["coins"] as JArray;

                    foreach (JObject item in array)
                    {
                        Coin? coin = db?.Coins.FirstOrDefault(coin => coin.Name == item["name"].ToString());
                        decimal price = (decimal)Double.Parse(item["price"].ToString());
                        if (coin != null)
                        {
                            if (coin.Price > price)
                            {
                                coin.isFall = true;
                            }
                            else
                            {
                                coin.isFall = false;
                            }
                            coin.Price = price;
                        }
                        else
                        {
                            coin = new Coin();
                            coin.Name = item["name"].ToString();
                            coin.Symbol = item["symbol"].ToString();
                            coin.MarketCap = item["marketCap"].ToString();
                            coin.Image = item["iconUrl"].ToString();
                            coin.Price = price;
							db?.Coins.Add(coin);
                        }
                    }
                    db?.SaveChanges();

                }
            }
            Thread.Sleep(10000);
        }
    }
}
#pragma warning restore CS8602 // –азыменование веро€тной пустой ссылки.


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSingleton<UserService>();
var app = builder.Build();

Thread RefreshCoinsThread = new Thread(() => RefreshingCoins(builder));
RefreshCoinsThread.Start();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSession();
app.UseStaticFiles();
app.MapRazorPages();
app.UseMiddleware<UserMiddalware>();



app.Run();
