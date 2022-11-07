using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //getAll, getById, insert, delete y update

         Task<IReadOnlyList<T>>  getAll();
         Task<T>  getById(int id);
         Task  insert(T entity);
         Task  delete(T entity);
         Task update(T entity);
         Task saveChanges();
        FixedTermDepositDTO GetFixedTransactionDetailById(FixedTermDepositEntity fixedDeposit);
    }
}
