using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Authorship
    {
        public int ID { get; set; }
        public Guid BookID { get; set; }

        public Guid AuthorID { get; set;  }

        public Book Book { get; set; }
        public Author Author { get; set; }

    }
}
