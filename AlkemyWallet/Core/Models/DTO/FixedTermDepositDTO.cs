namespace AlkemyWallet.Core.Models.DTO
{
    public class FixedTermDepositDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}