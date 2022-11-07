namespace AlkemyWallet.Core.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}
