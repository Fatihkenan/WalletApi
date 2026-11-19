namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email, string userName, IList<string> roles);
    }
}
