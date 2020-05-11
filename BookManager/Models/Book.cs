using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Book
    {

        [Key]
        public Guid BookID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Authorship> Authorships { get; set; }

        public Location Location { get; set; }


    }
}
