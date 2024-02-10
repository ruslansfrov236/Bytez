

using bytez.data.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;

namespace bytez.data.Concrete
{
    public class ContactCallWriteRepository : WriteRepository<ContactCall>, IContactCallWriteRepository
    {
        public ContactCallWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
