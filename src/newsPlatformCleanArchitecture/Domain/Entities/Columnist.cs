using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Columnist : Entity<Guid>
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public string Email { get; set; }
    public string LinkedinLink { get; set; }

    public ICollection<ColumnArticle> ColumnArticles { get; set; }
}
