using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class FixedTermDepositService : IFixedTermDepositServices
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FixedTermDepositService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

      
        public FixedTermDepositDTO GetFixedTransactionDetailById(FixedTermDepositEntity fixedDeposit)
        {
            if (fixedDeposit.UserId == _unitOfWork.UserRepository.getById(fixedDeposit.UserId).Id)
            {
                return _mapper.Map<FixedTermDepositDTO>(fixedDeposit);
            }
            else
                return null;
        }

      
    }
}