using MediatR;

namespace Application.Features.Auth.Login
{
    // String dönmesinin sebebi, şimdilik "Giriş Başarılı" veya "Hata" mesajı döneceğiz.
    // İleride burası string yerine Token modelimiz olacak.
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}