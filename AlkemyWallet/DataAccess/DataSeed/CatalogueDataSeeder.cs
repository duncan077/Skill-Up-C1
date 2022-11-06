using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class CatalogueDataSeeder
    {

        public static void CatalogueDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogueEntity>().HasData(
                    new { Id = 1, ProductDescription = "Descripcion Primera", Image = "https://api.com/imagen1", Points = 5,
                        IsDeleted = false
                    },
                    new { Id = 2,
                        ProductDescription = "Descripcion Segunda", Image = "https://api.com/imagen2", Points = 2,
                        IsDeleted = false
                    },
                    new { Id = 3,
                        ProductDescription = "Descripcion Tercera", Image = "https://api.com/imagen3", Points = 2,
                        IsDeleted = false
                    },
                    new { Id = 4,
                        ProductDescription = "Descripcion Cuarta", Image = "https://api.com/imagen4", Points = 4,
                        IsDeleted = false
                    },
                    new { Id = 5,
                        ProductDescription = "Descripcion Quita", Image = "https://api.com/imagen5", Points = 3,
                        IsDeleted = false
                    },
                    new { Id = 6,
                        ProductDescription = "Descripcion Sexta", Image = "https://api.com/imagen6", Points = 3,
                        IsDeleted = false
                    },
                    new { Id = 7,
                        ProductDescription = "Descripcion Séptima", Image = "https://api.com/imagen7", Points = 5,
                        IsDeleted = false
                    }
               );
        }

    }
}

