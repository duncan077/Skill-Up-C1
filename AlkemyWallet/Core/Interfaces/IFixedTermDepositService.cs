using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IFixedTermDepositService
    {
        Task delete(FixedTermDepositEntity entity);
        Task<IReadOnlyList<FixedTermDepositEntity>> getAll();
        Task<FixedTermDepositEntity> getById(int id);
        Task insert(FixedTermDepositEntity entity);
        Task saveChanges();
        Task update(FixedTermDepositEntity entity);
    }
}