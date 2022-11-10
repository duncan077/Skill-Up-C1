using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRolesRepository RolesRepository { get; }
        void Save();
    }
}
