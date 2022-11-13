using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IFixedTermDepositService
    {
        Task delete(FixedTermDepositEntity entity);
        Task<IReadOnlyList<FixedTermDepositEntity>> getAll();
        Task<FixedTermDepositEntity> getById(int id);
        Task saveChanges();
        Task update(FixedTermDepositEntity fixedTermDeposit);
        Task CreateFixedTermDeposit(CreateFixedTermDepositDTO model, string userName);

        Task<FixedTermDepositEntity> GetFixedTermDepositDetail(int idFixedTerm, string userName);

        Task<IReadOnlyList<FixedTermDepositEntity>> getTransactionsByUserId(int id);
        Task<PagedList<FixedTermDepositEntity>> getAllbyUser(PagesParameters rolesParams, string username);


        

    }
}
