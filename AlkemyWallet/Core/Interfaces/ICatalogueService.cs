using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ICatalogueService
    {

        Task delete(CatalogueEntity entity);
        Task<IReadOnlyList<CatalogueDTO>> getAll();
        Task<CatalogueEntity> getById(int id);
        Task insert(CatalogueEntity entity);
      
        Task update(CatalogueEntity entity);

        Task<IReadOnlyList<CatalogueDTO>> getAllSortByPoints();
        Task<IReadOnlyList<CatalogueDTO>> GetCatalogueByUserPoints(int points);
        Task<UserEntity> getUserByUserName(string userName);
        Task<List<CatalogueEntity>> getAllCatalogue();
    }



}
