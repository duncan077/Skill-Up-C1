using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
       
        public UserService(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
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
