using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IFixedTermDepositRepository : IGenericRepository<FixedTermDepositEntity>
    {
        Task<IReadOnlyList<FixedTermDepositEntity>> getFixedTermDepositByUserId(int id);
        Task<PagedList<FixedTermDepositEntity>> getAll(PagesParameters pageParams);
    }
}
