using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AlkemyWallet.Core.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserEntity> QueryAsync(Expression<Func<UserEntity, bool>> filter, Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> orderBy = null, Func<IQueryable<UserEntity>, IQueryable<UserEntity>> includes = null)
        {
            return (UserEntity)await _unitOfWork.UserRepository.QueryAsync(filter, orderBy, includes);
        }
        public async Task delete(UserEntity entity)
        {
            await _unitOfWork.UserRepository.delete(entity);
        }


        public async Task<IReadOnlyList<UserEntity>> getAll()
        {
            return await _unitOfWork.UserRepository.getAll();
        }

        public async Task<UserEntity> getById(int id)
        {
            return await _unitOfWork.UserRepository.getById(id);
        }
        public async Task<UserEntity> getByUserName(string userName)
        {
            return await _unitOfWork.UserRepository.getByUserName(userName);
        }

        public async Task insert(UserEntity entity)
        {
            await _unitOfWork.UserRepository.insert(entity);
        }

        public async Task saveChanges()
        {
            await _unitOfWork.UserRepository.saveChanges();
        }

        public async Task update(UserEntity entity)
        {
            await _unitOfWork.UserRepository.update(entity);
        }
    }
}
