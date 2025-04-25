namespace IT_Tools_Web.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
