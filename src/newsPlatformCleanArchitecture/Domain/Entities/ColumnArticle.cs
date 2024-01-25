using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ColumnArticle : Entity<Guid>
{
    public Guid ColumnistId { get; set; } 
    public Guid CategoryId { get; set; } 
    public string Title { get; set; }
    public string Content { get; set; }
    public string FeaturedImage { get; set; }

    public Columnist Columnist { get; set; }
    public Category Category { get; set; }
}
