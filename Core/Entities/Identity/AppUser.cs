

using Core.Entities.Cart;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime JoinedAt { get; set; }
        public Address Address { get; set; }
    }
}
