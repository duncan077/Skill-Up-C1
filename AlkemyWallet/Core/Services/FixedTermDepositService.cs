using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class FixedTermDepositService : IFixedTermDepositServices
    {
        private IUnitOfWork _unitOfWork;
        public FixedTermDepositService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task delete(FixedTermDepositEntity entity)
        {
            await _unitOfWork.FixedTermDepositRepository.delete(entity);
        }


        public async Task<IReadOnlyList<FixedTermDepositEntity>> getAll()
        {
            return await _unitOfWork.FixedTermDepositRepository.getAll();
        }

        public async Task<FixedTermDepositEntity> getById(int id)
        {
            return await _unitOfWork.FixedTermDepositRepository.getById(id);
        }

        public async Task insert(FixedTermDepositEntity entity)
        {
            await _unitOfWork.FixedTermDepositRepository.insert(entity);
        }

        public async Task saveChanges()
        {
            await _unitOfWork.FixedTermDepositRepository.saveChanges();
        }

        public async Task update(FixedTermDepositEntity entity)
        {
            await _unitOfWork.FixedTermDepositRepository.update(entity);
        }



    }
}