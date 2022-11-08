using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class FixedTermDepositService : IFixedTermDepositService
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

        public async Task saveChanges()
        {
            await _unitOfWork.FixedTermDepositRepository.saveChanges();
        }

        public async Task update(FixedTermDepositEntity entity)
        {
            await _unitOfWork.FixedTermDepositRepository.update(entity);
        }
        
        public FixedTermDepositEntity GetFixedTransactionDetailById(FixedTermDepositEntity fixedDeposit)
        {
            if (fixedDeposit.UserId == _unitOfWork.UserRepository.getById(fixedDeposit.UserId.Value).Id)
            {
                return  (fixedDeposit);
            }
            return null;
        }

        public async Task CreateFixedTermDeposit(CreateFixedTermDepositDTO model)
        {

            AccountsEntity UserAccount = _unitOfWork.AccountsRepository.getById(model.AccountId).Result;
            UserEntity User = _unitOfWork.UserRepository.getById(model.UserId).Result;

            if (UserAccount != null && User != null && model.ClosingDate >= DateTime.Now.AddDays(1)&& model.Amount>0)
            {
                if (UserAccount.Money >= model.Amount)
                {
                    UserAccount.Money -= model.Amount;
                    await _unitOfWork.AccountsRepository.update(UserAccount);
                    FixedTermDepositEntity NewFixedTermDepositEntity = new FixedTermDepositEntity();

                    NewFixedTermDepositEntity.User = User;
                    NewFixedTermDepositEntity.UserId = User.Id;
                    
                    
                    NewFixedTermDepositEntity.Account = UserAccount;
                    NewFixedTermDepositEntity.AccountId = UserAccount.Id;

                    NewFixedTermDepositEntity.Amount = model.Amount;
                    NewFixedTermDepositEntity.CreationDate = DateTime.Now;
                    NewFixedTermDepositEntity.ClosingDate = model.ClosingDate;
                    

                    await _unitOfWork.FixedTermDepositRepository.insert(NewFixedTermDepositEntity);
                    await _unitOfWork.Save();

                }
                else { throw new Exception("Insufficient balance"); }


            }
            else { throw new Exception("Incorrect Data - Check UserId, Account, Ammount and Closing Date. Remember that the closing Date must be greater than today"); }

            

        }



    }
}
