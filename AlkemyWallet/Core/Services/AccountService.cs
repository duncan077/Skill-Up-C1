using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.OpenApi.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using static AlkemyWallet.Entities.TransactionEntity;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IJWTAuthManager _jwtManager;

        public AccountService(IUnitOfWork unitOfWork, IJWTAuthManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _jwtManager = jwtManager;
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

        public async Task<TransferToAccountsDTO> TransferAccounts(TransferToAccountsDTO model, int id)
        {
            try
            {

                //if (!_jwtManager.VerifyPasswordHash(model.Password, _jwtManager.CreatePasswordHash(model.Password))) throw new ArgumentException("");

                var withdrawBalanceAccount = await _unitOfWork.AccountsRepository.getById(id);
                if (withdrawBalanceAccount is null) throw new ArgumentException("");

                var addBalanceAccount = await _unitOfWork.AccountsRepository.getById(model.ToAccountId);
                if (withdrawBalanceAccount is null) throw new ArgumentException("");

                withdrawBalanceAccount.Money -= model.Amount; 
                await _unitOfWork.AccountsRepository.update(withdrawBalanceAccount);

                addBalanceAccount.Money += model.Amount;
                await _unitOfWork.AccountsRepository.update(addBalanceAccount);

                var user = await _unitOfWork.UserRepository.getById(withdrawBalanceAccount.UserId);
                user.Points = (int)(model.Amount * (3 / 100));
                await _unitOfWork.UserRepository.update(user);

                var trans = new TransactionEntity(user.Id, withdrawBalanceAccount.Id, addBalanceAccount.Id, Typess.Topup, DateTime.Now,model.Amount, "Money Transfer");
                await _unitOfWork.TransactionRepository.update(trans);

                await _unitOfWork.Save();
            }
            catch (Exception err)
            {
                throw;
            }




            return model;
        }
    }
}
