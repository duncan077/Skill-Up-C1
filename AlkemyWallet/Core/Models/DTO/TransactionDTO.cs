using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        [Required]
        public int Id;
 
        [Required]
        public decimal Amount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string Types { get; set; } = "Payment";


        [Required]
        public int UserId { get; set; }
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int ToAccountId { get; set; }



    }
}
