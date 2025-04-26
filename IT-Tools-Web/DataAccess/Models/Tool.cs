namespace IT_Tools_Web.DataAccess.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true; // Mặc định là true
        public bool IsPremium { get; set; } = false; // Mặc định là false
        public ICollection<Favorite> Favorites { get; set; }
    }
}
