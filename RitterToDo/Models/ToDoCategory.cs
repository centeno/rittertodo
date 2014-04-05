using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RitterToDo.Models
{
    public class ToDoCategory : IOwnedEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        public ApplicationUser Owner { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
    }

    public class ToDoCategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
