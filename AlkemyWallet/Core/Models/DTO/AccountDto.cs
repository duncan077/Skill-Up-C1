using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public record AccountDto(DateTime CreationDate, decimal Money, bool IsBlocked, int UserId, int Id);
       
    
}
