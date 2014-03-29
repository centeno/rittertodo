using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RitterToDo.Models
{
    public class ToDo : IOwnedEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength=4)]
        public string Name { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Starred { get; set; }

        public bool Done { get; set; }

        [Required(ErrorMessage = "É necessário informar uma Categoria para o ToDo")]
        public ToDoCategory Category { get; set; }

        [Required(ErrorMessage = "É necessário informar o Dono do ToDo")]
        public ApplicationUser Owner { get; set; }

        [ForeignKey("Category")]
        public Guid ToDoCategoryId { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
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