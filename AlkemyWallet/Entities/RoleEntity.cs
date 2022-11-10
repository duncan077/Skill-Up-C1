using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class RoleEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}
