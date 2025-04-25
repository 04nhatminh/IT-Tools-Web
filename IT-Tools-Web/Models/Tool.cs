namespace IT_Tools_Web.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
