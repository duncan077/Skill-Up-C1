using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransferToAccountsDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int ToAccountId { get; set; }
        public int SourceAccount { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
