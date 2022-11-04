using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public  static partial class RoleEntitySeeder
    {

        public static void ConfigureMyEntity(this ModelBuilder modelbuilder) {

            modelbuilder.Entity<RoleEntity>().HasData(new RoleEntity { Id=1, IsDeleted=false, Name = "Admin", Description = "System Admin"  });
            modelbuilder.Entity<RoleEntity>().HasData(new RoleEntity { Id=2, IsDeleted = false, Name = "Regular", Description = "Regular User" });

        }

    }
}
