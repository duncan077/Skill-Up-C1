using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace AlkemyWallet.Entities
{
    public class FixedTermDepositEntity : EntityBase
    {



        
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity? User { get; set; }


       
        public int? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountsEntity? Account { get; set; }



        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }

    }
}
