using AlkemyWallet.Core.Interfaces;
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


        public async Task<IReadOnlyList<TransactionEntity>> getAll()
        {
            return await _unitOfWork.TransactionRepository.getAll();
        }

        public async Task<TransactionEntity> getById(int id)
        {
            return await _unitOfWork.TransactionRepository.getById(id);
        }

        public async Task<IReadOnlyList<TransactionEntity>> getTransactionsByUserId(int id)
        {
            return await _unitOfWork.TransactionRepository.getTransactionsByUserId(id);
        }

        public async Task insert(TransactionEntity entity)
        {
            await _unitOfWork.TransactionRepository.insert(entity);
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
