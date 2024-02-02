using bytez.business.Dto.Login;
using bytez.entity.Entities.Identity;

namespace bytez.business.Abstract
{
    public interface IAppUserService
    {
        Task<bool> LoginAsync(LoginDto model);
       
        Task<AppUser> RegistrationAsync(CreateRegistrationDto model);

        Task LogOutAsync();
    }
}
