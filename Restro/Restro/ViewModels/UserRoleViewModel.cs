using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class UserRoleViewModel
    {
        [Required]
        [Display(Name = "Select User")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Select Role")]
        public string RoleName { get; set; }
    }
}
