using ApexiBee.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ApexiBee.Persistance.EntityConfiguration
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public UserAccount UserAccount { get; set; } = null!;
    }
}
