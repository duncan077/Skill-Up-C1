namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //getAll, getById, insert, delete y update

        Task<IReadOnlyList<T>> getAll();
        Task<T> getById(int id);
        Task<int> insert(T entity);
        void delete(T entity);
        Task<int> update(T entity);

    }
}
