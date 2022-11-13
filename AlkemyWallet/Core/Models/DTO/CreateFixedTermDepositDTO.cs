using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class CreateFixedTermDepositDTO
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }


    }
}
