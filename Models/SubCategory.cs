using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListOfItems.Models
{
    public class SubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? SubCategoryName { get; set; }

        [Required]
        public int Avaliable { get; set; }

        [Required]
        public DateTime CreatedDate { get;  set; }=DateTime.Now;
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }
    }
}
