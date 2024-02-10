using bytez.business.Dto.Email;
using bytez.entity.Entities;

namespace bytez.business.Abstract
{
    public interface IEmailService
    {
        Task<List<Email>> GetEmailAllAsync();
        Task<Email> GetEmailByIdAsync(string id);

        Task<bool> Create(CreateEmailDto model);
        Task<bool> Update(UpdateEmailDto model);
        Task<bool> Delete(string id);
    }
}