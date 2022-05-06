﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVDRental.Areas.Identity.Data;
using DVDRental.Models;
using Microsoft.AspNetCore.Authorization;

namespace DVDRental.Controllers
{
    public class LoanTypesController : Controller
    {
        private readonly AppDBContext _context;

        public LoanTypesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: LoanTypes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoanTypes.ToListAsync());
        }

        // GET: LoanTypes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanType = await _context.LoanTypes
                .FirstOrDefaultAsync(m => m.LoanTypeNumber == id);
            if (loanType == null)
            {
                return NotFound();
            }

            return View(loanType);
        }

        // GET: LoanTypes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoanTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("LoanTypeNumber,LoanCategory,LoanDuration")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loanType);
        }

        // GET: LoanTypes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanType = await _context.LoanTypes.FindAsync(id);
            if (loanType == null)
            {
                return NotFound();
            }
            return View(loanType);
        }

        // POST: LoanTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("LoanTypeNumber,LoanCategory,LoanDuration")] LoanType loanType)
        {
            if (id != loanType.LoanTypeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanTypeExists(loanType.LoanTypeNumber))
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
            return View(loanType);
        }

        // GET: LoanTypes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanType = await _context.LoanTypes
                .FirstOrDefaultAsync(m => m.LoanTypeNumber == id);
            if (loanType == null)
            {
                return NotFound();
            }

            return View(loanType);
        }

        // POST: LoanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanType = await _context.LoanTypes.FindAsync(id);
            _context.LoanTypes.Remove(loanType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool LoanTypeExists(int id)
        {
            return _context.LoanTypes.Any(e => e.LoanTypeNumber == id);
        }
    }
}
