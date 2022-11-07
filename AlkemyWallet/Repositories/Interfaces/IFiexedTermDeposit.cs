using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IFixedTermDepositServices : IGenericRepository<FixedTermDepositEntity>
    {
        public FixedTermDepositDTO GetFixedTransactionDetailById(FixedTermDepositEntity fixedDeposit);
    }
}