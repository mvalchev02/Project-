using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class UserWithRolesViewModel
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
