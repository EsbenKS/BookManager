using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Loan
    {
        // https://softdevpractice.com/blog/many-to-many-ef-core/
        [Key]
        public int LoanID { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime LoanEndDate { get; set; }

        public Guid BookID { get; set; }
        public Book Book { get; set; }

        public Guid AuthorId  { get; set; }
        public Author Author { get; set;  }

        public bool isLoanActive()
        {
            // Return true if Enddate is later than now - else false. 
            return LoanEndDate > DateTime.Now; 
        }

    }
}
