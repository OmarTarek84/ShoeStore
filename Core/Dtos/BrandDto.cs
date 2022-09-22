using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class BrandUpdateDto : BrandInDto
    {
        [Required]
        public int Id { get; set; }
    }

    public class BrandInDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class BrandOutDto : OutDto
    {
        public string Name { get; set; }
    }
}
