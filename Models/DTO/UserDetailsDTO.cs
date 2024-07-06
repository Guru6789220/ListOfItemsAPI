using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ListOfItems.Models.DTO
{
    public class UserDetailsDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? UserName { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [MaxLength(26)]
        public string? Password { get; set; }

        [ForeignKey("RoleId")]
        public int ConsumerType { get; set; }

    }
}
