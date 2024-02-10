using bytez.business.Abstract;
using bytez.business.Dto.ContactCall;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Concrete
{
    public class ContactCallService : IContactCallService
    {
        readonly private IContactCallReadRepository _contactCallReadRepository;
        readonly private IContactCallWriteRepository _contactCallWriteRepository;

        public ContactCallService(IContactCallReadRepository contactCallReadRepository, IContactCallWriteRepository contactCallWriteRepository)
        {
            _contactCallReadRepository = contactCallReadRepository;
            _contactCallWriteRepository = contactCallWriteRepository;
        }
        public async Task<ContactCall> ContactCallById(string id)
        => await _contactCallReadRepository.GetByIdAsync(id);

        public async Task<bool> Create(CreateContactCallDto model)
        {
            ContactCall contactCall = new ContactCall()
            {
                Title=model.Title,
                Description=model.Description,
                Phone=model.Phone

            };
            await _contactCallWriteRepository.AddAsync(contactCall);
            await _contactCallWriteRepository.SaveAsync();

            return true;


        }

        public async Task<bool> Delete(string id)
        {
            await _contactCallWriteRepository.RemoveAsync(id);
            await _contactCallWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<ContactCall>> GetContactCallAllAsync()
        {
            List<ContactCall> contactCalls = await _contactCallReadRepository.GetAll().ToListAsync();

            return contactCalls;
        }

        public async Task<bool> Update(UpdateContactCallDto model)
        {
            var contactCall = await _contactCallReadRepository.GetByIdAsync(model.id);
            contactCall.Title = model.Title;
            contactCall.Description = model.Description;
            contactCall.Phone = model.Phone;
            _contactCallWriteRepository.Update(contactCall);
            await _contactCallWriteRepository.SaveAsync();

            return true;
        }
    }
}
