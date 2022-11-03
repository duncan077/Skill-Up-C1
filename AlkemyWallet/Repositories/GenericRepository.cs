using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public UnitOfWork UnitOfWork;

        public GenericRepository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public Task delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> getById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
