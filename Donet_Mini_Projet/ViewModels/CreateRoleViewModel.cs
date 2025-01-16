using System.ComponentModel.DataAnnotations;

namespace Donet_Mini_Projet.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]

        public string RoleName { get; set; }
    }
}
