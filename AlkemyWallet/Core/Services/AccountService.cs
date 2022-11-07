using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;   
            _unitOfWork = unitOfWork;
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

        public async Task<List<AccountDto>> ListedAccounts()
        {

            return _mapper.Map<List<AccountDto>>(await _unitOfWork.AccountsRepository.getAll()) ?? new List<AccountDto>();
        }

       
    }
}
