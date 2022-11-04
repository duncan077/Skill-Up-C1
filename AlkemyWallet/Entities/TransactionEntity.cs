using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class TransactionEntity : EntityBase { 
        decimal Ammount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }
        public string[] Type { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual AccountsEntity Account { get; set; }

        public int ToAccountId { get; set; }

        public TransactionEntity()
        {
            Type = new string[2] { "topup", "payment" };
            Account = new AccountsEntity();
            User = new UserEntity();
        }

        public TransactionEntity(int userId, int accountId, int toAccountId) : this()
        {
            UserId = userId;
            AccountId = accountId;
            ToAccountId = toAccountId;  
        }

        public TransactionEntity(int userId, int accountId, int toAccountId,DateTime date, decimal ammount, string concept) : this(userId, accountId, toAccountId)
        {
            Date = date;
            Ammount = ammount;
            Concept = concept;
        }
    }
}
