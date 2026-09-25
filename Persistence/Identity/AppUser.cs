using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
