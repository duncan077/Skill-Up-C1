using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
       public decimal Amount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }

        public string Types { get; set; } = String.Empty;
      

      
        public int UserId { get; set; }
      
        public int AccountId { get; set; }
     
       
        public int ToAccountId { get; set; }
    


     
    }
}
