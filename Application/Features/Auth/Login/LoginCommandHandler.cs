using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthService _authService;

        // Dependency Injection: Sistemden gerçek AuthService nesnesini istiyoruz
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        // MediatR bu komutu yakaladığında bu metodu tetikleyecek
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // İşi, demin yazdığımız ve veritabanına bağlanan AuthService'e devrediyoruz
            var result = await _authService.LoginAsync(request);
            return result;
        }
    }
}