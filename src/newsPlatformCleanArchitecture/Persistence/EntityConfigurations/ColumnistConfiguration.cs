using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ColumnistConfiguration : IEntityTypeConfiguration<Columnist>
{
    public void Configure(EntityTypeBuilder<Columnist> builder)
    {
        builder.ToTable("Columnists").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name");
        builder.Property(c => c.Biography).HasColumnName("Biography");
        builder.Property(c => c.ProfilePicture).HasColumnName("ProfilePicture");
        builder.Property(c => c.Email).HasColumnName("Email");
        builder.Property(c => c.LinkedinLink).HasColumnName("LinkedinLink");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}