using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class AccountsEntity : EntityBase
    {
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; }
        public bool IsBlocked { get; set; }
       
        [ForeignKey("user_id")]
        public virtual UserEntity User { get; set; }

        [Column("Id")]
        public int UserId { get; set; }

    }

}


