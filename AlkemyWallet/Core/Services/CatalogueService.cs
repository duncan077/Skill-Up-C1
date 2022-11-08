using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class CatalogueService : ICatalogueService
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CatalogueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        public async Task delete(CatalogueEntity entity)
        {
             await _unitOfWork.CatalogueRepository.delete(entity);
        }

        public async Task<IReadOnlyList<CatalogueEntity>> getAll()
        {
            return await _unitOfWork.CatalogueRepository.getAll();
        }

        public async Task<CatalogueEntity> getById(int id)
        {

            return await _unitOfWork.CatalogueRepository.getById(id);
          
        }

        public async Task insert(CatalogueEntity entity)
        {
             await _unitOfWork.CatalogueRepository.insert(entity);
        }

        public async Task update(CatalogueEntity entity)
        {
            await _unitOfWork.CatalogueRepository.update(entity);
        }
    }
}
