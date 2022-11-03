namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //getAll, getById, insert, delete y update

        Task<IReadOnlyList<T>> getAll();
        Task<T> GetByIdAsync(int id);
        Task<int> UpdateAsinc(T entity);
        Task<int> InsertAsinc(T entity);
        void DeleteAsinc(T entity);

    }
}
