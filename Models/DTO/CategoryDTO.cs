using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListOfItems.Models.DTO
{
    public class CategoryDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(30)]
        public string? CategoryName { get; set; }
        [Required]
        public int Avaliable { get; set; }

    }
}
