using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RitterToDo.Models
{
    public interface IOwnedEntity : IEntity
    {
        ApplicationUser Owner { get; set; }
    }
}