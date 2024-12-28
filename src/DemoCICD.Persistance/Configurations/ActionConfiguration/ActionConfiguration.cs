using DemoCICD.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistance.Configurations.ActionConfiguration;
public class ActionConfiguration : IEntityTypeConfiguration<Action>
{
    public void Configure(EntityTypeBuilder<Action> builder)
    {
        builder.ToTable(TableNames.Actions);
        builder.HasKey(t => t.Id);
    }
}
