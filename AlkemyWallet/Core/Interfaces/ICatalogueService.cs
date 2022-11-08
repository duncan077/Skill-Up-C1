using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ICatalogueService
    {

        Task delete(CatalogueEntity entity);
        Task<IReadOnlyList<CatalogueEntity>> getAll();
        Task<CatalogueEntity> getById(int id);
        Task insert(CatalogueEntity entity);
      
        Task update(CatalogueEntity entity);


    }



}
