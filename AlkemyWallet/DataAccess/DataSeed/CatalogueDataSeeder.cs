using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class CatalogueDataSeeder
    {

        public static void CatalogueDataSeed(this ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<CatalogueEntity>().HasData(
                    new { Id = 1, Product_description = "Descripcion Primera", Image = "https://api.com/imagen1", Points = 5 },
                    new { Id = 2, Product_description = "Descripcion Segunda", Image = "https://api.com/imagen2", Points = 2 },
                    new { Id = 3, Product_description = "Descripcion Tercera", Image = "https://api.com/imagen3", Points = 2 },
                    new { Id = 4, Product_description = "Descripcion Cuarta", Image = "https://api.com/imagen4", Points = 4 },
                    new { Id = 5, Product_description = "Descripcion Quita", Image = "https://api.com/imagen5", Points = 3 },
                    new { Id = 6, Product_description = "Descripcion Sexta", Image = "https://api.com/imagen6", Points = 3 },
                    new { Id = 7, Product_description = "Descripcion Séptima", Image = "https://api.com/imagen7", Points = 5 }
               );*/
        }

    }
}

