namespace AlkemyWallet.DataAccess
{
    public class WalletDbContext :DbContext
    {
        public WalletDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
