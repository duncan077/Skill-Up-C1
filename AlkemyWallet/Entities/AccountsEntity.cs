using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class AccountsEntity : BaseEntity
    {
        public DateTime creationDate { get; set; }
        public decimal money { get; set; }
        public bool isBlocked { get; set; }

        [ForeignKey("user_id")]
        public virtual UserEntity userId { get; set; }

    }

}


