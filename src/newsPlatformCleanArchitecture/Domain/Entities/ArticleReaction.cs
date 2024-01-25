using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ArticleReaction : Entity<Guid>
{
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; } 
    public string VoterIdentifier { get; set; }
    public Article Article { get; set; }

}