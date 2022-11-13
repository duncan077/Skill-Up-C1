using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface ICatalogueRepository:IGenericRepository<CatalogueEntity>
    {
        Task<IReadOnlyList<CatalogueEntity>> getCatalogueOrderByPoints();
        Task<IReadOnlyList<CatalogueEntity>> getCatalogueByUserPoints(int points);
    }
}
