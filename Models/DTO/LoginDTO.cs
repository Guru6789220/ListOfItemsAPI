using System.ComponentModel.DataAnnotations;

namespace ListOfItems.Models.DTO
{
    public class LoginDTO
    {
        public string? UserName { get; set; }
        public string? Emailid { get; set; }

        [Required]
        public string? Password { get; set; }
        public string? RoleType { get; set; }

        public string? ErrorMsg { get; set; }

    }
}
