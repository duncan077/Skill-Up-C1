using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace AlkemyWallet.Entities
{
    public class FixedTermDeposit : BaseEntity
    {
     
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }



        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }

    }
}
