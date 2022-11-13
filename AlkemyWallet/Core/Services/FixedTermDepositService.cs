using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using System.Security.Cryptography;

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
            entity.IsDeleted = true;
            await _unitOfWork.FixedTermDepositRepository.update(entity);
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

        public async Task update(FixedTermDepositEntity fixedTermDeposit)
        {
            

            fixedTermDeposit.User = await _unitOfWork.UserRepository.getById((int)fixedTermDeposit.UserId);
            fixedTermDeposit.Account = await  _unitOfWork.AccountsRepository.getById((int)fixedTermDeposit.AccountId);
            await _unitOfWork.FixedTermDepositRepository.update(fixedTermDeposit);
            await _unitOfWork.Save();

            

        }
        
        public async Task<FixedTermDepositEntity> GetFixedTermDepositDetail(int idFixedTermDeposit, string userName)
        {
            
            var fixedTermDeposit = await _unitOfWork.FixedTermDepositRepository.getById((int)idFixedTermDeposit);
            var user = await _unitOfWork.UserRepository.getByUserName(userName);


            if (fixedTermDeposit!=null && user!=null && fixedTermDeposit.UserId == user.Id)
            {
                fixedTermDeposit.User = user;

                return  (fixedTermDeposit);
            }
            return null;
        }

        public async Task CreateFixedTermDeposit(CreateFixedTermDepositDTO model, string userName)
        {
            
            AccountsEntity UserAccount = await _unitOfWork.AccountsRepository.getById(model.AccountId);
            UserEntity User = await _unitOfWork.UserRepository.getByUserName(userName);

            if (UserAccount != null && User != null && UserAccount.UserId == User.Id && model.ClosingDate >= DateTime.Now.AddDays(1)&& model.Amount>0)
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
            else { throw new Exception("Incorrect Data - Check AccountId, Ammount and Closing Date. Remember that the closing Date must be greater than today"); }

            

        }

        public async Task<IReadOnlyList<FixedTermDepositEntity>> getTransactionsByUserId(int id)
        {
            return await _unitOfWork.FixedTermDepositRepository.getFixedTermDepositByUserId(id);
        }

        public async Task<PagedList<FixedTermDepositEntity>> getAllbyUser(PagesParameters pagesParams, string username)
        {
            try 
            { 
            
            UserEntity usuario = await _unitOfWork.UserRepository.getByUserName(username);

            var AllFixedTermDeposit = await _unitOfWork.FixedTermDepositRepository.getAll(pagesParams, usuario.Id);
                return AllFixedTermDeposit;
            
            }
            catch{ throw new Exception("An error occured"); }

        }
      
    }

}
