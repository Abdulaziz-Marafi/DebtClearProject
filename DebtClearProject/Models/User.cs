

using Microsoft.AspNetCore.Identity;

namespace DebtClearProject.Models
{
    public class User : IdentityUser
    {

        public decimal Balance { get; set; }

        public string? ProfilePicture { get; set; }
        public string Img { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string FName { get; internal set; }
        public string LName { get; internal set; }
    }
}
