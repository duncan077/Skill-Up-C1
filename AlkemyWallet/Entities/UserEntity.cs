using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class UserEntity : EntityBase
    {
        public UserEntity()
        {
            Account=new AccountsEntity();
        }
  
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public int Points { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleEntity Role { get; set; }
        [Column("rol_id")]
        public int RoleId { get; set; }

        public virtual AccountsEntity Account { get; set; }
 
    }
}
