using BookManager.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Author : Person
    {

        public ICollection<Authorship> Authorships { get; set; }



    }
}
