using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListOfItems.Models.DTO
{
    public class SubCategoryDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string? SubCategoryName { get; set; }
        [Required]
        public int Avaliable { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

    }
}
