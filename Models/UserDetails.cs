using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListOfItems.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? UserName { get; set; }

       
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [MaxLength(26)]
        public string? Password { get; set; }

        public string? Gender { get; set; }

        public string? CreatedDate { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public int ConsumerType { get; set; }

        public Roles Roles { get; set; }
    }
}
