using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class AccountsEntity : EntityBase
    {
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; }
        public bool IsBlocked { get; set; }
       
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

    }

}


