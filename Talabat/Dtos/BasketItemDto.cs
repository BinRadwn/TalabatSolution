using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string productName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be one item at Least!!")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must be Greater than Zero!!")]
        public decimal Price { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }
    }
}