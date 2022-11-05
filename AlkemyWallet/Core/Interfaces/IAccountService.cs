using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService:IGenericRepository<AccountsEntity>
    {
      
    }
}
