namespace EvenRegistretion.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Phone { get; set; }= string.Empty;
        public DateTime RegisteredAt { get; set; }
    }
}

