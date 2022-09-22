
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class ReviewInDto: ReviewUpdateDto
    {


    }

    public class ReviewUpdateDto
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "must specify a rating from 1 to 5")]
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class ReviewOutDto
    {
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}