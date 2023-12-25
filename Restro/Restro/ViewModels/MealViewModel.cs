using System.ComponentModel.DataAnnotations;

namespace Restro.ViewModels
{
    public class MealViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double Rate { get; set; }
        // Photo properties
        public string? PhotoName { get; set; }
        public string? Path { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
