using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
