using System.ComponentModel.DataAnnotations;

namespace ListOfItems.Models.DTO
{
    public class LoginDTO
    {
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? RoleType { get; set; }


    }
}
