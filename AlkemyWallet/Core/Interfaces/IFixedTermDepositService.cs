using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IFixedTermDepositService
    {
        Task delete(FixedTermDepositEntity entity);
        Task<IReadOnlyList<FixedTermDepositEntity>> getAll();
        Task<FixedTermDepositEntity> getById(int id);
        Task saveChanges();
        Task update(UpdateFixedTermDepositDTO model);
        Task CreateFixedTermDeposit(CreateFixedTermDepositDTO model);

        FixedTermDepositEntity GetFixedTransactionDetailById( FixedTermDepositEntity fixedDeposit);

        Task<IReadOnlyList<FixedTermDepositEntity>> getTransactionsByUserId(int id);

    }
}
