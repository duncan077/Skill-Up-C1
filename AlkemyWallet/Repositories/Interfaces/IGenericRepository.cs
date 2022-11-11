using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;
using System.Linq.Expressions;

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
        Task<IReadOnlyList<T>> getAllPaged(PagesParameters parameters);
    }
}
