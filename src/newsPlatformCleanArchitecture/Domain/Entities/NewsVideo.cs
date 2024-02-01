using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class NewsVideo : Entity<Guid>
{
    public string Title { get; set; }
    public string VideoURL { get; set; }
  
}
