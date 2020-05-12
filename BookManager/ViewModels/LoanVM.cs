using BookManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.ViewModels
{
    public class LoanVM : Loan
    {
        public List<Loan> ActiveLoansList { get; set; }
        public List<Loan> OverdueLoansList { get; set; }
    }
}
