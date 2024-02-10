using System.ComponentModel.DataAnnotations;
using E = bytez.entity.Entities;
namespace bytez.business.Dto.ContactWall
{
    public class CreateContactWallDto
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }

    }
}
