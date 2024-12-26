using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity;
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public DateTime? DateOfBirthi { get; set; }

    public bool? IsDirectorr { get; set; }

    public bool? IsHeadOfDepartment { get; set; }

    public Guid? MangerID { get; set; }

    public Guid? PositionID { get; set; }

    public int IsRecepitient { get; set; }

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }

    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }

    public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
}
