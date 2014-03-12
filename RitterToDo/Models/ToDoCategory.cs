using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RitterToDo.Models
{
    public class ToDoCategory : IOwnedEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
