using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity;
public class AppRole : IdentityRole<Guid>
{
    public string Description { get; set; }

    public string RoleCode { get; set; }

    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

    public virtual ICollection<IdentityRoleClaim<Guid>> Claim { get; set; }

    public virtual ICollection<Permission<Guid>> Permissions { get; set; }
}
