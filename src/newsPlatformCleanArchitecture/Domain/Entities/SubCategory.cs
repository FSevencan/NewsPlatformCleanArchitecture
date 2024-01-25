using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class SubCategory : Entity<Guid>
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Article>? Articles { get; set; }
}
