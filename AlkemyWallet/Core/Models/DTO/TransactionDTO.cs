using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        decimal Ammount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }

        public string Types { get; set; } = String.Empty;
      

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual AccountsEntity Account { get; set; }
        [ForeignKey("AccountTo")]
        public int ToAccountId { get; set; }
        public virtual AccountsEntity AccountTo { get; set; }


        public TransactionDTO()
        {
            User=new UserEntity();
            Account = new AccountsEntity();
            AccountTo = new AccountsEntity();
        }
    }
}
