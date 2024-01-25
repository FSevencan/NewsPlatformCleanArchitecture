using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PollVoteConfiguration : IEntityTypeConfiguration<PollVote>
{
    public void Configure(EntityTypeBuilder<PollVote> builder)
    {
        builder.ToTable("PollVotes").HasKey(pv => pv.Id);

        builder.Property(pv => pv.Id).HasColumnName("Id").IsRequired();
        builder.Property(pv => pv.PollId).HasColumnName("PollId");
        builder.Property(pv => pv.PollOptionId).HasColumnName("PollOptionId");
        builder.Property(pv => pv.VoterIdentifier).HasColumnName("VoterIdentifier");
     
        builder.Property(pv => pv.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pv => pv.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pv => pv.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pv => !pv.DeletedDate.HasValue);
    }
}