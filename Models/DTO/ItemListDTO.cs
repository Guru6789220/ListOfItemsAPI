using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ListOfItems.Models.DTO
{
    public class ItemListDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey("SubCategoryId")]
        public int SubCategoryId { get; set; }

        [Required]
        public string? ItemName { get; set; }

        [Required]
        public string? ItemDesc { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Discount { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
      
        public string? CategoryName { get; set; }
        
        public string? SubCategoryName { get; set; }

     

    }

}
