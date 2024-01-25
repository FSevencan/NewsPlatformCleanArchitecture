using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PollOptionConfiguration : IEntityTypeConfiguration<PollOption>
{
    public void Configure(EntityTypeBuilder<PollOption> builder)
    {
        builder.ToTable("PollOptions").HasKey(po => po.Id);

        builder.Property(po => po.Id).HasColumnName("Id").IsRequired();
        builder.Property(po => po.PollId).HasColumnName("PollId");
        builder.Property(po => po.OptionText).HasColumnName("OptionText");
        builder.Property(po => po.VoteCount).HasColumnName("VoteCount");
      
        builder.Property(po => po.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(po => po.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(po => po.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(po => !po.DeletedDate.HasValue);
    }
}