using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class AccountsEntity : EntityBase
    {
  
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; }
        public bool IsBlocked { get; set; } = false;
       
       
        public int UserId { get; set; }
      
        public virtual UserEntity User { get; set; }


    }

}


