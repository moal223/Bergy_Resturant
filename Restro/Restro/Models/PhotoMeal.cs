using System.ComponentModel.DataAnnotations;

namespace Restro.Models
{
    public class PhotoMeal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
