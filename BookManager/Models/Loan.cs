using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Loan
    {
   
        [Key]
        public int LoanID { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Loan Date")]
        public DateTime LoanStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        public DateTime LoanEndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LoanReturnedDate { get; set; }
        public Guid BookID { get; set; }
        public Book Book { get; set; }

        public Guid LoanerID  { get; set; }
        public Loaner Loaner { get; set;  }
        public bool isLoanActive()
        {
            return LoanReturnedDate == DateTime.MinValue || LoanReturnedDate == null;
        }

        public bool isLoanOverdue()
        {
            // Return true if Enddate is later than now - else false. 
            return LoanEndDate < DateTime.Now && isLoanActive(); 
        }

    }
}
