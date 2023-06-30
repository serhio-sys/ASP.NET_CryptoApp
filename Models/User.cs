namespace CryptoApp.Models
{
    public class User 
    {
        public int Id { get; set; }
        
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public bool IsAdmin { get; set; }
        public List<Coin> Coins { get; set; } = new List<Coin>();

        public override string ToString()
        {
            return $"{Username} | {Email} | {Password}";
        }
    }

}
