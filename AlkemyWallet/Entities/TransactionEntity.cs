using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class TransactionEntity : EntityBase { 
        public decimal Ammount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }

        public string Types { get; set; }=String.Empty;


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual AccountsEntity Account { get; set; }

        public int ToAccountId { get; set; }

        public TransactionEntity()
        {

            Types = string.Empty;

            Account = new AccountsEntity();
            User = new UserEntity();
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

        public TransactionEntity(int userId, int accountId, int toAccountId,Typess type,DateTime date, decimal ammount, string concept) : this(userId, accountId, toAccountId, type)
        {
            Date = date;
            Ammount = ammount;
            Concept = concept;
        }
        public enum Typess
        {
            Topup,
            Payment
        }
    }
}
