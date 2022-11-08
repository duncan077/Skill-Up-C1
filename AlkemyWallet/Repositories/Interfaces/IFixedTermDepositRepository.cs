using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IFixedTermDepositRepository : IGenericRepository<FixedTermDepositEntity>
    {
        Task<IReadOnlyList<FixedTermDepositEntity>> getFixedTermDepositByUserId(int id);
    }
}
