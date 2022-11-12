using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using System.Linq.Expressions;


namespace AlkemyWallet.Core.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;

            _unitOfWork = unitOfWork;
        }
       
        public async Task delete(UserEntity entity)
        {
            await _unitOfWork.UserRepository.delete(entity);
        }


        /*public async Task<IReadOnlyList<UserEntity>> getAll()
        {
            return await _unitOfWork.UserRepository.getAll();
        }*/
        
        public async Task<IReadOnlyList<UserDTO>> getAll()
        {
            return _mapper.Map<List<UserDTO>>(await _unitOfWork.UserRepository.getAll()) ?? new List<UserDTO>();
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
        public async Task<CatalogueDTO> GetCatalogueById(int idProduct)
        {
            return _mapper.Map<CatalogueDTO>(await _unitOfWork.CatalogueRepository.getById(idProduct));
        }

        public async Task<AccountsEntity> GetAccountByID(int id)
        {
            return await _unitOfWork.AccountsRepository.getById(id);
        }

        public async Task blockAccount(AccountsEntity account)
        {
            account.IsBlocked = true;
            await _unitOfWork.AccountsRepository.update(account);
            await _unitOfWork.AccountsRepository.saveChanges();
        }


    }
}
