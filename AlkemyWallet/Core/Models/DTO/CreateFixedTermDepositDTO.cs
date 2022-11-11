namespace AlkemyWallet.Core.Models.DTO
{
    public class CreateFixedTermDepositDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }


    }
}
