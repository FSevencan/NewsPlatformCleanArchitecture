using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class PollOption : Entity<Guid>
{
    public Guid PollId { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
    public Poll Poll { get; set; }
}