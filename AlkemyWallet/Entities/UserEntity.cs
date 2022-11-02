using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class UserEntity : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public int Points { get; set; }

        [ForeignKey("role_id")]
        public virtual RoleEntity Role { get; set; }

        [Column("Id")]
        public int RoleId { get; set; }
    }
}
