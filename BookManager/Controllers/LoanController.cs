using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManager.Data;
using BookManager.Models;
using BookManager.ViewModels;

namespace BookManager.Controllers
{
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
         
            List<Loan> ActiveLoans = new List<Loan>();  
            List<Loan> OverdueLoans = new List<Loan>();
                        
            List<Loan> Loans =   await _context.Loans
                .OrderBy(d => d.LoanStartDate)
                .Include(l => l.Book)
                .Include(l => l.Loaner)
                .ToListAsync();

            foreach (var loan in Loans)
            {
                // Active loan
                if (loan.isLoanActive() == true)
                {
                    ActiveLoans.Add(loan);
                }

                // Overdue Loans
                if (loan.isLoanOverdue() == true)
                {
                    OverdueLoans.Add(loan);
                }

            }

            LoanVM LoanVM = new LoanVM
            {
                ActiveLoansList = ActiveLoans,
                OverdueLoansList = OverdueLoans
            };
            return View(LoanVM);
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Loaner)
                .FirstOrDefaultAsync(m => m.LoanID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title");
            ViewData["LoanerID"] = new SelectList(_context.Loaners.Where(l => l.Active), "ID", "FullName");
            var loanVM = new Loan
            {
                LoanStartDate = DateTime.Now,
                LoanEndDate = DateTime.Now.AddDays(14)
            };

            return View(loanVM);
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanID,LoanStartDate,LoanEndDate,BookID,LoanerID")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", loan.BookID);
            ViewData["LoanerID"] = new SelectList(_context.Loaners.Where(l => l.Active), "ID", "FullName", loan.LoanerID);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", loan.BookID);
            ViewData["LoanerID"] = new SelectList(_context.Loaners, "ID", "FullName", loan.LoanerID);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanID,LoanStartDate,LoanEndDate,BookID,LoanerID")] Loan loan)
        {
            if (id != loan.LoanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"]   = new SelectList(_context.Books, "BookID", "Title", loan.BookID);
            ViewData["LoanerID"] = new SelectList(_context.Loaners.Where(l => l.Active), "ID", "FullName", loan.LoanerID);
            return View(loan);
        }

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Loaner)
                .FirstOrDefaultAsync(m => m.LoanID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Loan/Return/5
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Loaner)
                .FirstOrDefaultAsync(m => m.LoanID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            loan.LoanReturnedDate = DateTime.Now;
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanID == id);
        }
    }
}
