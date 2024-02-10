using bytez.business.Abstract;
using bytez.business.Dto.ContactWall;
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
    public class ContactWallService : IContactWallService
    {
        readonly private IContactWallReadRepository _contactWallReadRepository;
        readonly private IContactWallWriteRepository _contactWallWriteRepository;

        public ContactWallService(IContactWallReadRepository contactWallReadRepository, IContactWallWriteRepository contactWallWriteRepository)
        {
            _contactWallReadRepository = contactWallReadRepository;
            _contactWallWriteRepository = contactWallWriteRepository;
        }
        public async Task<ContactWall> ContactWallById(string id)
        => await _contactWallReadRepository.GetByIdAsync(id);


        public async Task<bool> Create(CreateContactWallDto model)
        {
            ContactWall contactWall = new ContactWall()
            {
                Title=model.Title,
                Description = model.Description
            };

            await _contactWallWriteRepository.AddAsync(contactWall);
            await _contactWallWriteRepository.SaveAsync();

            return true;
        }
        public async Task<bool> Delete(string id)
        {
            await _contactWallWriteRepository.RemoveAsync(id);
            await _contactWallWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<ContactWall>> GetContactWallAllAsync()
        {
            List<ContactWall> contactWalls = await _contactWallReadRepository.GetAll()
                                                                             .ToListAsync();
            return contactWalls;
        }

        public async Task<bool> Update(UpdateContactWallDto model)
        {
            var contactWall = await _contactWallReadRepository.GetByIdAsync(model.Id);
            contactWall.Title = model.Title;
            contactWall.Description = model.Description;

            _contactWallWriteRepository.Update(contactWall);
            await _contactWallWriteRepository.SaveAsync();
            return true;
        }
    }
}
