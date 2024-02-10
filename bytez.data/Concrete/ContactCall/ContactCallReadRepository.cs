using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.data.Concrete
{
    public class ContactCallReadRepository : ReadRepository<ContactCall>, IContactCallReadRepository
    {
        public ContactCallReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
