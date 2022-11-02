using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using System.Security.Principal;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public class CatalogueDataSeeder
    {
        public IUnitOfWork _uowDb;
        public CatalogueDataSeeder(IUnitOfWork uowDb)
        {
            _uowDb = uowDb;
        }

        public void Seed()
        {
            if (!_uowDb.Catalogue.Any())
            {
                var catalogue = new List<Catalogue>()
                {
                    new Catalogue { Id = 1 , Product_description = "Descripcion Primera", Image = "https://api.com/imagen1" , Points = 5 },
                    new Catalogue { Id = 2 , Product_description = "Descripcion Segunda", Image = "https://api.com/imagen2" , Points = 2 },
                    new Catalogue { Id = 3 , Product_description = "Descripcion Tercera", Image = "https://api.com/imagen3" , Points = 2 },
                    new Catalogue { Id = 4 , Product_description = "Descripcion Cuarta", Image = "https://api.com/imagen4" , Points = 4 },
                    new Catalogue { Id = 5 , Product_description = "Descripcion Quita", Image = "https://api.com/imagen5" , Points = 3 },
                    new Catalogue { Id = 6 , Product_description = "Descripcion Sexta", Image = "https://api.com/imagen6" , Points = 3 },
                    new Catalogue { Id = 7 , Product_description = "Descripcion Séptima", Image = "https://api.com/imagen7" , Points = 5 }
                };

                _uowDb.Catalogue.AddRange(catalogue);
                _uowDb.SaveChanges();
            }
}

    }
}
