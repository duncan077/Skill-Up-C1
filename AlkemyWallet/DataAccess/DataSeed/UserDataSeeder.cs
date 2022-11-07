using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class UserDataSeeder
    {
        public static void UserDataSeed(this ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<UserEntity>().HasData(
    new { Id=0,FirstName="Duncan", LastName="Caceres", RoleId=0,Points=0,Email="dcacerescartasso@gmail.com",Password="test1234" },
    new { Id=1, FirstName = "Diego", LastName = "Rodrigues", RoleId = 1, Points = 425, Email = "diegor89@gmail.com", Password = "DiegoTest333" },
    new { Id=2, FirstName = "Lucas", LastName = "Gonzales", RoleId = 1, Points = 5245, Email = "lgonzales53@gmail.com", Password = "Boca4784" },
    new { Id=3, FirstName = "admin", LastName = "admin", RoleId = 0, Points = 0, Email = "alkwallet@gmail.com", Password = "admin1234" });
        */}
    }
}
