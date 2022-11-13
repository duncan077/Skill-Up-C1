using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class TransactionDataSeeder
    {
        public static void TransactionDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEntity>().HasData(
                new
                {
                    Id = 1,
                    Amount =  Convert.ToDecimal(100.00),

                    Concept = "Pago",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Types = "payment",
                    UserId = 1,
                    AccountId = 1,
                    ToAccountId = 2,
                    IsDeleted = false
                },
                new
                {
                    Id = 2,

                    Amount = Convert.ToDecimal(2500.30),
                    Concept = "Compra del dia",
                    Date = DateTime.UtcNow.AddMonths(-3),
                    Types = "payment",
                    UserId = 2,
                    AccountId = 4,
                    ToAccountId = 2,
                    IsDeleted = false
                }
            );
        }
    }
}
