using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using System.Data;

namespace AlkemyWallet.Core.Services
{
    public class RolesService : IRolesService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task delete(RoleEntity entity)
        {
            await _unitOfWork.RolesRepository.delete(entity);
            await _unitOfWork.RolesRepository.saveChanges();
        }


        public async Task<IReadOnlyList<RoleEntity>> getAll()
        {
            return await _unitOfWork.RolesRepository.getAll();
        }

        public async Task<IReadOnlyList<RoleEntity>> getAll(RolesParameters rolesParams)
        {
            if (rolesParams is null) return await getAll();
            return await _unitOfWork.RolesRepository.getAll(rolesParams);
        }

        public async Task<RoleEntity> getById(int id)
        {
            return await _unitOfWork.RolesRepository.getById(id);
        }

        public async Task<RolesDTO> insert(RolesDTO entity)
        {
            try
            {
                var rol = _mapper.Map<RoleEntity>(entity);
                await _unitOfWork.RolesRepository.saveChanges();
                return _mapper.Map<RolesDTO>(rol);
            }
            catch (Exception err)
            {
                throw;
            }
           
        }

        public async Task saveChanges()
        {
            await _unitOfWork.RolesRepository.saveChanges();
        }

        public async Task<RolesDTO> update(RoleEntity entity)
        {
            try
            {
                await _unitOfWork.RolesRepository.saveChanges();
                return _mapper.Map<RolesDTO>(entity);
            }
            catch (Exception err)
            {

                throw;
            }

        }
    }
}
