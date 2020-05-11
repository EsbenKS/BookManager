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
    public class AuthorshipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorshipController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authorship
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Authorships.Include(a => a.Author)
                .OrderBy(c => c.Author.FirstName)
                .Include(a => a.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Authorship/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // GET: Authorship/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName");
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title");
            return View();
        }

        // POST: Authorship/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BookID,AuthorID")] Authorship authorship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName", authorship.AuthorID);
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // GET: Authorship/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships.FindAsync(id);
            if (authorship == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName", authorship.AuthorID);
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // POST: Authorship/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookID,AuthorID")] Authorship authorship)
        {
            if (id != authorship.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorshipExists(authorship.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName", authorship.AuthorID);
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // GET: Authorship/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // POST: Authorship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorship = await _context.Authorships.FindAsync(id);
            _context.Authorships.Remove(authorship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorshipExists(int id)
        {
            return _context.Authorships.Any(e => e.ID == id);
        }
    }
}
