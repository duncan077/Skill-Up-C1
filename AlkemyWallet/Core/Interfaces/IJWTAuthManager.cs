namespace AlkemyWallet.Core.Interfaces
{
    public interface IJWTAuthManager
    {
        byte[] CreatePasswordHash(string password);
        string CreateRandomToken();
        string CreateToken(string userName, string role);
        byte[] GetSalt();
        bool VerifyPasswordHash(string password, byte[] passwordHash);
    }
}