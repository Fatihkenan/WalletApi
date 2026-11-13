using Application.Features.Auth.Login; // Bunu yukarıya eklemeyi unutma
using Application.Features.Auth.Register;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterCommand request);

        // Yeni eklediğimiz kural:
        Task<string> LoginAsync(LoginCommand request);
    }
}