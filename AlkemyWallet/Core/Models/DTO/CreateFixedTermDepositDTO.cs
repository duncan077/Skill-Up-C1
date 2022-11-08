namespace AlkemyWallet.Core.Models.DTO
{
    public class CreateFixedTermDepositDTO
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }


    }
}
