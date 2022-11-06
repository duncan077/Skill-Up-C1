using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class FixedTermDepositDataSeeder
    {
        public static void FixedTermDepositDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FixedTermDepositEntity>().HasData(
                new FixedTermDepositEntity() {Id=1, UserId = 1, AccountId = 1, Amount = 1200, CreationDate = new DateTime(2022,11,05,15,08,55), ClosingDate = new DateTime(2023, 11, 05, 00, 00, 00) },
                new FixedTermDepositEntity() {Id=2, UserId = 2, AccountId = 2, Amount = 2000, CreationDate = new DateTime(2018, 01, 14, 09, 10, 55), ClosingDate = new DateTime(2022, 12, 18, 00, 00, 00) },
                new FixedTermDepositEntity() {Id=3, UserId = 3, AccountId = 2, Amount = 2100, CreationDate = new DateTime(2020, 06, 15, 16, 09, 25), ClosingDate = new DateTime(2023, 01, 15, 00, 00, 00) },
                new FixedTermDepositEntity() {Id=4, UserId = 4, AccountId = 4, Amount = 1400, CreationDate = new DateTime(2021, 08, 20, 12, 35, 15), ClosingDate = new DateTime(2023, 08, 08, 00, 00, 00) }
                );
        }
    }
}
