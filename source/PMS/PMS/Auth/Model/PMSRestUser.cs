using Microsoft.AspNetCore.Identity;

namespace PMS.Auth.Model
{
    public class PMSRestUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ForceRelogin { get; set; }

    }
}
