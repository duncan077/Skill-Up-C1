using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class TransactionEntity : EntityBase {
        [Required]
        public decimal Amount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }

        public string Types { get; set; }=String.Empty;

        [Required]
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity? User { get; set; }
        [Required]
        
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountsEntity? Account { get; set; }

        [Required]
        public int ToAccountId { get; set; }

        public TransactionEntity()
        {

            Types = string.Empty;

           
        }

        public TransactionEntity(int userId, int accountId, int toAccountId,Typess type) : this()
        {
            switch (type)
            {
                case Typess.Topup:
                    Types = "Topup";
                    break;
                case Typess.Payment:
                    Types = "Payment";
                    break;
            }
            UserId = userId;
            AccountId = accountId;
            ToAccountId = toAccountId;  
        }

        public TransactionEntity(int userId, int accountId, int toAccountId,Typess type,DateTime date, decimal amount, string concept) : this(userId, accountId, toAccountId, type)
        {
            Date = date;
            Amount = amount;
            Concept = concept;
        }
        public enum Typess
        {
            Topup,
            Payment
        }
    }
}
