using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Infrastructure.DataAccess.Contexts.User.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.Property(u => u.Login)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Password).IsRequired().HasMaxLength(64);

        builder.HasIndex(u => u.Login)
            .IsUnique();
    }
}