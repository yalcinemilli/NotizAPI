namespace NotizAPI.Models
{
    public class Notiz
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}