namespace AlkemyWallet.Core.Interfaces
{
    public interface IRolesServices<T>
    {
        Task<IReadOnlyList<T>> getAll();
        Task<T> getById(int id);
        Task insert(T entity);
        Task delete(T entity);
        Task update(T entity);
        Task saveChanges();
    }
}
