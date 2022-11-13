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
        AccountId = 1,Points =0,Email="dcacerescartasso@gmail.com",Password= "n+W6mDcjIDP3V3m4gmtpilWoSUfwxTUPxfgWBqjaWSp3CKBhJHa1h8/nRvBrjcyC6m4kXr34JMsnh8+11BVvCA==",
        IsDeleted = false
    },
    new { Id=2, FirstName = "Diego", LastName = "Rodrigues", RoleId = 2,
        AccountId = 2, Points = 425, Email = "diegor89@gmail.com", Password = "NT+A8xbqsp9rvSmBK4x2LOEXYl0rybyXRRpSLxaYvQASAgnTT2khS+iQLu+h3RqcmuiBPY/KERnQ09DL/Clquw==",
        IsDeleted = false
    },
    new { Id=3, FirstName = "Lucas", LastName = "Gonzales", RoleId = 2,
        AccountId = 3, Points = 5245, Email = "lgonzales53@gmail.com", Password = "s3zUJfByMg6NgecUoiNbmAJ2AqX2OhDHCAbUuzA1boUkB984WxdbcWo43OR+HfvVf7pKQyRAs8/aVD/vmVtrjQ==",
        IsDeleted = false
    },
    new { Id=4, FirstName = "admin", LastName = "admin", RoleId = 1,
        AccountId=4, Points = 0, Email = "alkwallet@gmail.com", Password = "kZMHhy1p18L9BkfrXHiAj/mJqGlPfHZ76pxCXQWSj+8oXUyC/9KkjBm7v+FEKJQj12ArNf1xgehR5D1TPLWy7Q==",
        IsDeleted = false
    });
        }
    }
}
