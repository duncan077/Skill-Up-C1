using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using System.Security.Claims;

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
            await _unitOfWork.Save();
        }

        public async Task<IReadOnlyList<CatalogueDTO>> getAll()
        {
            //return await _unitOfWork.CatalogueRepository.getAll();
            return _mapper.Map<List<CatalogueDTO>>(await _unitOfWork.CatalogueRepository.getAll()) ?? new List<CatalogueDTO>();
        }

        public async Task<IReadOnlyList<CatalogueDTO>> getAllSortByPoints()
        {
            return _mapper.Map<List<CatalogueDTO>>(await _unitOfWork.CatalogueRepository.getCatalogueOrderByPoints()) ?? new List<CatalogueDTO>();

        }

        public async Task<CatalogueEntity> getById(int id)
        {

            return await _unitOfWork.CatalogueRepository.getById(id);
          
        }


        public async Task insert(CatalogueEntity entity)
        {
             await _unitOfWork.CatalogueRepository.insert(entity);
            await _unitOfWork.Save();
        }

        public async Task update(CatalogueEntity entity)
        {
            await _unitOfWork.CatalogueRepository.update(entity);
            await _unitOfWork.Save();
        }

        public async Task<IReadOnlyList<CatalogueDTO>> GetCatalogueByUserPoints(int points)
        {
            return _mapper.Map<List<CatalogueDTO>>(await _unitOfWork.CatalogueRepository.getCatalogueByUserPoints(points));
        }
        public async Task<UserEntity> getUserByUserName(string userName)
        {
            return await _unitOfWork.UserRepository.getByUserName(userName);
        }

        public async Task<List<CatalogueEntity>> getAllCatalogue()
        {
            return (List<CatalogueEntity>)await _unitOfWork.CatalogueRepository.getAll();
        }
    }
}
