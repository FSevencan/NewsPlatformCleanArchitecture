using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Advertisement : Entity<Guid>
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public int ClickCount { get; set; }
    public int ViewCount { get; set; }
   
}