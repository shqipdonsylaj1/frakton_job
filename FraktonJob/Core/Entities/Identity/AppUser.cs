using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        #region Properties
        public string DisplayName { get; set; }
        public Users Users { get; set; }
        #endregion
    }
}
