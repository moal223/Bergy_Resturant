using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
