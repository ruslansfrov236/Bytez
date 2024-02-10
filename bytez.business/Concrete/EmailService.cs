using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bytez.business.Abstract;
using bytez.business.Dto.Email;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{
    public class EmailService : IEmailService
    {
        readonly private IEmailReadRepository _emailReadRepository;
        readonly private IEmailWriteRepository _emailWriteRepository;

        public EmailService(IEmailReadRepository emailReadRepository , IEmailWriteRepository emailWriteRepository )
        {
            _emailReadRepository=emailReadRepository;
            _emailWriteRepository=emailWriteRepository;
        }
        public async Task<bool> Create(CreateEmailDto model)
        {
            await _emailWriteRepository.AddAsync(new Email() { EmailAddress = model.EmailAddress });
            await _emailWriteRepository.SaveAsync();
            return true;

        }

        public async Task<bool> Delete(string id)
        {


            await _emailWriteRepository.RemoveAsync(id);
            await _emailWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Email>> GetEmailAllAsync()
        {
            List<Email> emails = await _emailReadRepository.GetAll().ToListAsync();

            return emails;
        }

        public async Task<Email> GetEmailByIdAsync(string id)
        => await _emailReadRepository.GetByIdAsync(id);
        public async Task<bool> Update(UpdateEmailDto model)
        {
            var email = await _emailReadRepository.GetByIdAsync(model.id);
            model.EmailAddress = email.EmailAddress;
            _emailWriteRepository.Update(email);
            await _emailWriteRepository.SaveAsync();

            return true;
        }
    }
}
