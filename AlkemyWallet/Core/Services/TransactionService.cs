using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task delete(TransactionEntity entity)
        {
            await _unitOfWork.TransactionRepository.delete(entity);
        }


        public async Task<PagedList<TransactionEntity>> getAll(int page, string username)
        {
            var user = await _unitOfWork.UserRepository.getByUserName(username);
            var param = new PagesParameters();
            param.PageNumber = page;
            return await _unitOfWork.TransactionRepository.getAll(param, user.Id);
        }

        public async Task<TransactionEntity> getById(int id)
        {
            return await _unitOfWork.TransactionRepository.getById(id);
        }

 

        public async Task insert(TransactionEntity entity)
        {
            await _unitOfWork.TransactionRepository.insert(entity);
        }
        public async Task CreateTransaction(TransactionEntity entity)
        {
            var user = await _unitOfWork.UserRepository.GetById(entity.UserId);
            if(user != null)
            {
                if(user.Accounts.Where(a=>a.Id==entity.AccountId&&a.IsBlocked!=true).Any())
                {
                    var accountTo = await _unitOfWork.AccountsRepository.getById(entity.ToAccountId);
                    if (accountTo != null)
                    {
                        entity.Date=DateTime.Now;
                        await _unitOfWork.TransactionRepository.insert(entity);
                        await _unitOfWork.Save();
                    }
                    else
                        throw new ArgumentException("Error: Transfer Account not found.");
                }
                else
                    throw new ArgumentException(" Error: User and account are different");
            }
            else
                throw new ArgumentException("Error: User not found.");
           
        }
        public async Task DeleteTransaction(int id)
        {
            var transaction = await _unitOfWork.TransactionRepository.getById(id);
            if(transaction != null)
            {
                transaction.IsDeleted = true;
                await _unitOfWork.TransactionRepository.update(transaction);
                await _unitOfWork.Save();
            }
            throw new ArgumentException("Error: Transaction not found.");
        }

        public async Task UpdateTransaction(TransactionEntity entity, int id)
        {
            try
            {
               
                await _unitOfWork.TransactionRepository.update(entity);
                await _unitOfWork.Save();

            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task saveChanges()
        {
            await _unitOfWork.TransactionRepository.saveChanges();
        }

        public async Task update(TransactionEntity entity)
        {
            await _unitOfWork.TransactionRepository.update(entity);
        }
    }
}
