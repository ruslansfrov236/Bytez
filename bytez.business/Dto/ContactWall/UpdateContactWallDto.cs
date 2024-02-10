using System.ComponentModel.DataAnnotations;
using E = bytez.entity.Entities;

namespace bytez.business.Dto.ContactWall
{
    public class UpdateContactWallDto
    {
        public string  Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }

        
    }
}
