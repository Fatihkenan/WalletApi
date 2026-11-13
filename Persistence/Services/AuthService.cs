using Application.Features.Auth.Login;
using Application.Features.Auth.Register;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // --- KAYIT OL (REGISTER) METODU ---
        public async Task<string> RegisterAsync(RegisterCommand request)
        {
            var user = new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return $"Kullanıcı {request.Username} başarıyla kaydedildi.";
            }

            return $"Kullanıcı {request.Username} kaydedilirken bir hata oluştu.";
        }

        // --- GİRİŞ YAP (LOGIN) METODU ---
        public async Task<string> LoginAsync(LoginCommand request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return "Hata: Kullanıcı bulunamadı.";
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordCorrect)
            {
                return "Hata: Girdiğiniz şifre yanlış.";
            }

            return "Başarılı: Sisteme giriş yapıldı!";
        }
    }
}