namespace IT_Tools_Web.DataAccess.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
