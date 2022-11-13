using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AlkemyWallet.Core.Models.DTO
{
    public class FixedTermDepositItemDTO
    {

        public int Id { get; set;}
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal Amount { get; set; }

        
    }
}
