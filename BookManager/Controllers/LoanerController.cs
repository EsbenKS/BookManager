using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManager.Data;
using BookManager.Models;

namespace BookManager.Controllers
{
    public class LoanerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loaner
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loaners
                .OrderBy(l => l.FirstName)
                .ToListAsync());
        }

        // GET: Loaner/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaner = await _context.Loaners
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaner == null)
            {
                return NotFound();
            }

            return View(loaner);
        }

        // GET: Loaner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loaner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Active,ID,FirstName,LastName")] Loaner loaner)
        {
            if (ModelState.IsValid)
            {
                loaner.ID = Guid.NewGuid();
                _context.Add(loaner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaner);
        }

        // GET: Loaner/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaner = await _context.Loaners.FindAsync(id);
            if (loaner == null)
            {
                return NotFound();
            }
            return View(loaner);
        }

        // POST: Loaner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Active,ID,FirstName,LastName")] Loaner loaner)
        {
            if (id != loaner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanerExists(loaner.ID))
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
            return View(loaner);
        }

        // GET: Loaner/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaner = await _context.Loaners
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaner == null)
            {
                return NotFound();
            }

            return View(loaner);
        }

        // POST: Loaner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var loaner = await _context.Loaners.FindAsync(id);
            _context.Loaners.Remove(loaner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanerExists(Guid id)
        {
            return _context.Loaners.Any(e => e.ID == id);
        }
    }
}
