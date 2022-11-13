namespace AlkemyWallet.Core.Interfaces
{
    public interface IJWTAuthManager
    {
        string CreatePasswordHash(string password);
        string CreateRandomToken();
        string CreateToken(string userName, string role);
        byte[] GetSalt();
        bool VerifyPasswordHash(string password, string passwordHash);
    }
}