using Application.Features.Auth.Register;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterCommand request);
    }
}
