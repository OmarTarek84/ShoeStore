
namespace Core.Dtos
{
    public class UserOutDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CartItemsCount { get; set; }
        public AddressOutDto Address { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
