using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RitterToDo.Models
{
    public class ToDo : IOwnedEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Starred { get; set; }

        public bool Done { get; set; }

        public ToDoCategory Category { get; set; }

        public ApplicationUser Owner { get; set; }
    }

    public class ToDoViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Starred { get; set; }

        public string CategoryName { get; set; }
        public bool Done { get; set; }
    }
}