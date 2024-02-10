using System.ComponentModel.DataAnnotations;
using E = bytez.entity.Entities;
namespace bytez.business.Dto.Email
{
    public class CreateEmailDto
    {
         
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
