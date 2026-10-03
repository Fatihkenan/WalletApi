using MediatR;

namespace Application.Features.Auth.Register
{

    // IRequest<string>: Bu komut çalıştığında geriye sadece bir metin (string) mesaj döneceğiz demek.
    public class RegisterCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
