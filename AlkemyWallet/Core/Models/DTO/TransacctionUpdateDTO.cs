using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransacctionUpdateDTO
    {
       public  int Id;
        public bool IsDeleted { get; set; }
        public decimal Ammount { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string Concept { get; set; }
        public DateTime Date { get; set; }

        public string Types { get; set; } = String.Empty;



        public int UserId { get; set; }

        public int AccountId { get; set; }


        public int ToAccountId { get; set; }

    }
}
