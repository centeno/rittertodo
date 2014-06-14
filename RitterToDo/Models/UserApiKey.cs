using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RitterToDo.Models
{
    public class UserApiKey
    {
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Key]
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string ApiKey { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string AppName { get; set; }

    }
}
