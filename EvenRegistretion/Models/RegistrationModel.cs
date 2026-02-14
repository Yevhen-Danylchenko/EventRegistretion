using System.ComponentModel.DataAnnotations;

namespace EvenRegistretion.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? Phone { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }

}

