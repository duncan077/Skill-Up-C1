using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services.ResourceParameters;
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
            try
            {
                await _unitOfWork.RolesRepository.delete(entity);
                await _unitOfWork.RolesRepository.saveChanges();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

        }


        public async Task<IReadOnlyList<RoleEntity>> getAll()
        {
            try
            {
                return await _unitOfWork.RolesRepository.getAll();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<IReadOnlyList<RoleEntity>> getAll(PagesParameters rolesParams)
        {
            try
            {
                return await _unitOfWork.RolesRepository.getAll(rolesParams);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<RoleEntity> getById(int id)
        {
            try
            {
                return await _unitOfWork.RolesRepository.getById(id);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<RolesDTO> insert(RolesDTO entity)
        {
            try
            {
                var rol = _mapper.Map<RoleEntity>(entity);

                await _unitOfWork.RolesRepository.insert(rol);

                await _unitOfWork.RolesRepository.saveChanges();
                return _mapper.Map<RolesDTO>(rol);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
           

        public async Task saveChanges()
        {
            try
            {
                await _unitOfWork.RolesRepository.saveChanges();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
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
                throw new Exception(err.Message);
            }

        }
    }
}
