using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class RolesService : IRolesServices
    {
        private IUnitOfWork _unitOfWork;
        public RolesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task delete(RoleEntity entity)
        {
            await _unitOfWork.RolesRepository.delete(entity);
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

        public async Task insert(RoleEntity entity)
        {
            await _unitOfWork.RolesRepository.insert(entity);
        }

        public async Task saveChanges()
        {
            await _unitOfWork.RolesRepository.saveChanges();
        }

        public async Task update(RoleEntity entity)
        {
            await _unitOfWork.RolesRepository.update(entity);
        }
    }
}
