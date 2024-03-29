﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using moment3_efc.Data;
using moment3_efc.Models;

namespace moment3_efc.Controllers
{
    public class LoansController : Controller
    {
        private readonly BookContext _context;

        public LoansController(BookContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var bookContext = _context.Loans.Include(l => l.Book).Include(l => l.Borrower);
            return View(await bookContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "BorrowerId", "BorrowerName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,BookId,BorrowerId,LoanDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", loan.BookId);
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "BorrowerId", "BorrowerName", loan.BorrowerId);
            return View(loan);
        }*/

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,BookId,BorrowerId,LoanDate")] Loan loan)
        {
            // get latest loan for specific book
            var latestLoan = await _context.Loans
                .Where(l => l.BookId == loan.BookId)
                .OrderByDescending(l => l.LoanDate)
                .FirstOrDefaultAsync();

            // Check if there is an existing loan on book
            if (latestLoan != null && latestLoan.LoanDate != null)
            {
                ModelState.AddModelError(string.Empty, "Boken är redan utlånad.");
            }
            else
            {
                // if no loan exists on book, add loan
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Add(loan);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Ett fel uppstod vid skapandet av lånet: " + ex.Message);
                    }
                }
            }

            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", loan.BookId);
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "BorrowerId", "BorrowerName", loan.BorrowerId);
            return View(loan);
        }


        // GET: Loans/Edit/5
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", loan.BookId);
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "BorrowerId", "BorrowerName", loan.BorrowerId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,BookId,BorrowerId,LoanDate")] Loan loan)
        {
            if (id != loan.LoanId)
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
                    if (!LoanExists(loan.LoanId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", loan.BookId);
            ViewData["BorrowerId"] = new SelectList(_context.Borrowers, "BorrowerId", "BorrowerName", loan.BorrowerId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }
    }
}
