using System.ComponentModel.DataAnnotations;

namespace EvenRegistretion.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опис обов'язковий")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Місце проведення обов'язкове")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата обов'язкова")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
