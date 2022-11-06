using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class UserDataSeeder
    {
        public static void UserDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
    new { Id=1,FirstName="Duncan", LastName="Caceres", RoleId=1,
        AccountId = 1,Points =0,Email="dcacerescartasso@gmail.com",Password="test1234",
        IsDeleted = false
    },
    new { Id=2, FirstName = "Diego", LastName = "Rodrigues", RoleId = 2,
        AccountId = 2, Points = 425, Email = "diegor89@gmail.com", Password = "DiegoTest333",
        IsDeleted = false
    },
    new { Id=3, FirstName = "Lucas", LastName = "Gonzales", RoleId = 2,
        AccountId = 3, Points = 5245, Email = "lgonzales53@gmail.com", Password = "Boca4784",
        IsDeleted = false
    },
    new { Id=4, FirstName = "admin", LastName = "admin", RoleId = 1,
        AccountId=4, Points = 0, Email = "alkwallet@gmail.com", Password = "admin1234",
        IsDeleted = false
    });
        }
    }
}
