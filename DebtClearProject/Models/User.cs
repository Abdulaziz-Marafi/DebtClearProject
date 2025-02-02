

using Microsoft.AspNetCore.Identity;

namespace DebtClearProject.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal Balance { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
