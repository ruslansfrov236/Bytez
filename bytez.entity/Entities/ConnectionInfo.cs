using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using bytez.entity.Entities.Customer;
namespace bytez.entity.Entities{

  public class ConnectionInfo:BaseEntity{

    public string? Title {get ; set;}
    public string FilePath {get ; set;}
    public string? Description {get ; set;}

    [NotMapped]
    public IFormFile File {get ; set;}
  }
}