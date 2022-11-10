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
        public string Concept { get; set; } = "Miscellaneous Concepts";
        [Required]
        public string Types { get; set; }
    }
}
