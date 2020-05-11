using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Location
    {
        public Guid ID { get; set; }

        public String Description { get; set;}

        public Guid BookID { get; set; }

        public Book Book { get; set; }


    }
}
