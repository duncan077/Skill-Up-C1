using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static AlkemyWallet.Entities.TransactionEntity;


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

        public async Task<PagedList<AccountsEntity>> getAll(int page)
        {
            PagesParameters parameters = new PagesParameters();
            parameters.PageNumber = page;
            return await _unitOfWork.AccountsRepository.getAll(parameters);
        }

        public async Task<AccountsEntity> getById(int id)
        {
            return await _unitOfWork.AccountsRepository.getById(id);
        }

        public async Task insert(AccountsEntity entity)
        {
            await _unitOfWork.AccountsRepository.insert(entity);
            await _unitOfWork.Save();
        }

        public async Task<List<AccountDto>> ListAccounts()
        {

            return _mapper.Map<List<AccountDto>>(await _unitOfWork.AccountsRepository.getAll()) ?? new List<AccountDto>();
        }

        public async Task saveChanges()
        {
            await _unitOfWork.AccountsRepository.saveChanges();
        }

        public async Task TransferAccounts(TransferToAccountsDTO model, int id, string userName)
        {
            int pointsPercentage = 2;
            try
            {

                var user = await _unitOfWork.UserRepository.getByUserName(userName);

                var withdrawBalanceAccount = await _unitOfWork.AccountsRepository.getById(id);
                if (withdrawBalanceAccount.UserId != user.Id) throw new ArgumentException("The account does not Correspond to the Logged User.");

                var addBalanceAccount = await _unitOfWork.AccountsRepository.getById(model.ToAccountId);
                if (withdrawBalanceAccount is null) throw new ArgumentException("Please, Enter a valid account for the recipient.");

                if(addBalanceAccount != withdrawBalanceAccount)
                {
                    if ((withdrawBalanceAccount.Money - model.Amount) < 0) throw new ArgumentException("Not enough available balance.");
                    withdrawBalanceAccount.Money -= model.Amount;
                    await _unitOfWork.AccountsRepository.update(withdrawBalanceAccount);
                    pointsPercentage = 3;
                }

                addBalanceAccount.Money += model.Amount;
                await _unitOfWork.AccountsRepository.update(addBalanceAccount);


                user.Points += (int)(model.Amount * (pointsPercentage / 100));
                await _unitOfWork.UserRepository.update(user);
                var type = new Typess();
                if (model.Types == "Topup") { type = Typess.Topup; } else type = Typess.Payment;

                var trans = new TransactionEntity(user.Id, withdrawBalanceAccount.Id, addBalanceAccount.Id, type, DateTime.Now, model.Amount, model.Concept);
                await _unitOfWork.TransactionRepository.update(trans);
                
                await _unitOfWork.Save();
            }
            catch (Exception err)
            {
                throw;
            }


        }

        public async Task update(AccountsEntity account)
        {
            await _unitOfWork.AccountsRepository.update(account);
            await _unitOfWork.Save();
        }

        public async Task delete(AccountsEntity entity)
        {
            try
            {
                await _unitOfWork.AccountsRepository.delete(entity);
                await _unitOfWork.AccountsRepository.saveChanges();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

        }

        public async Task DeleteAccount(AccountsEntity account)
        {
            account.IsDeleted = true;
            await _unitOfWork.AccountsRepository.update(account);
            await _unitOfWork.Save();     
        }


    }
}
