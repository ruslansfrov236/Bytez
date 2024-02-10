using bytez.entity.Entities.Customer;
using bytez.entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.entity.Entities
{
    public class Comment : BaseEntity
    {
        public Blog? Blog { get; set; }

        public Guid BlogId { get; set; }

        public AppUser? AppUser { get; set; }

        public string? AppUserId { get; set; }

        public string? Commenty { get; set; }

    }
}
