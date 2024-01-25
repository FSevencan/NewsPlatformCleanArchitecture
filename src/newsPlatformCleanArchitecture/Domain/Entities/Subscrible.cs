using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Subscrible : Entity<Guid>
{
    public string Email { get; set; }
    public bool IsConfirmed { get; set; } = false;
}
