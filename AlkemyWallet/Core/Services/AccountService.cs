using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class AccountService:IAccountService
    {
        private IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task delete(AccountsEntity entity)
        {
            await _unitOfWork.AccountsRepository.delete(entity);
        }


        public async Task<IReadOnlyList<AccountsEntity>> getAll()
        {
            return await _unitOfWork.AccountsRepository.getAll();
        }

        public async Task<AccountsEntity> getById(int id)
        {
            return await _unitOfWork.AccountsRepository.getById(id);
        }

        public async Task insert(AccountsEntity entity)
        {
            await _unitOfWork.AccountsRepository.insert(entity);
        }

        public async Task saveChanges()
        {
             _unitOfWork.Save();
        }

        public async Task update(AccountsEntity entity)
        {
            await _unitOfWork.AccountsRepository.update(entity);
        }
    }
}
