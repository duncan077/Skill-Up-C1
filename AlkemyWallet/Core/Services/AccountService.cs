using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using static AlkemyWallet.Entities.TransactionEntity;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
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

        public async Task TransferAccounts(TransferToAccountsDTO model, int id, string userName)
        {
            try
            {

                var user = await _unitOfWork.UserRepository.getByUserName(userName);

                var withdrawBalanceAccount = await _unitOfWork.AccountsRepository.getById(id);
                if (withdrawBalanceAccount.UserId != user.Id) throw new ArgumentException("The account does not Correspond to the Logged User.");
                if ((withdrawBalanceAccount.Money - model.Amount) < 0) throw new ArgumentException("Not enough available balance.");

                var addBalanceAccount = await _unitOfWork.AccountsRepository.getById(model.ToAccountId);
                if (withdrawBalanceAccount is null) throw new ArgumentException("Please, Enter a valid account for the recipient.");

                withdrawBalanceAccount.Money -= model.Amount;
                await _unitOfWork.AccountsRepository.update(withdrawBalanceAccount);

                addBalanceAccount.Money += model.Amount;
                await _unitOfWork.AccountsRepository.update(addBalanceAccount);


                user.Points = (int)(model.Amount * (3 / 100));
                await _unitOfWork.UserRepository.update(user);
                var type = new Typess();
                if (model.Types == "Topup") {type = Typess.Topup;} else type = Typess.Payment;

                var trans = new TransactionEntity(user.Id, withdrawBalanceAccount.Id, addBalanceAccount.Id, type, DateTime.Now, model.Amount, model.Concept);
                await _unitOfWork.TransactionRepository.update(trans);

                await _unitOfWork.Save();
            }
            catch (Exception err)
            {
                throw;
            }


        }
    }
}
