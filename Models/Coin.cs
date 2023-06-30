namespace CryptoApp.Models
{
    public class Coin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string MarketCap { get; set; }
        public bool isFall { get; set; } = false;
        public List<User> Users { get; set; } = new List<User>();
    }
}
