namespace AlkemyWallet.Core.Models.DTO
{
    public class AccountUpdateDto
    {
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; }
        public bool IsBlocked { get; set; } = false;
        public int UserId { get; set; }

    }
}
