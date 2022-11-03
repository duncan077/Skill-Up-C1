using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UnitOfWork _unitOfWork;

        public GenericRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
