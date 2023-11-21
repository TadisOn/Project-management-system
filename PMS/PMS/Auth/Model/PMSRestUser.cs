using Microsoft.AspNetCore.Identity;

namespace PMS.Auth.Model
{
    public class PMSRestUser : IdentityUser
    {
        public bool ForceRelogin { get; set; }

    }
}
