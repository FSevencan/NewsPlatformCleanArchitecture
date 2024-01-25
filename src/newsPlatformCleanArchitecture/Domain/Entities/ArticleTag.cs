using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ArticleTag : Entity<Guid>
{
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }
    public Article Article { get; set; }
    public Tag Tag { get; set; }
}
