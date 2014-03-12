using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RitterToDo.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
