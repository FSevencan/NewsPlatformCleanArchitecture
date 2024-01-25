using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class PollVote : Entity<Guid>
{
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string VoterIdentifier { get; set; } // IP adresi,çerez,localstroge vs.

    public PollOption PollOption { get; set; }
}