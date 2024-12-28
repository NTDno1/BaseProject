using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCICD.Persistence.Configurations;

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {


        builder.ToTable(TableNames.Permissions);

        builder.HasKey(x => new { x.RoleId, x.FunctionId, x.ActionId });
    }
}
