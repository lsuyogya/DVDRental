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
    public class MembershipCategoriesController : Controller
    {
        private readonly AppDBContext _context;

        public MembershipCategoriesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: MembershipCategories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipCategories.ToListAsync());
        }

        // GET: MembershipCategories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategory = await _context.MembershipCategories
                .FirstOrDefaultAsync(m => m.MembershipCategoryNumber == id);
            if (membershipCategory == null)
            {
                return NotFound();
            }

            return View(membershipCategory);
        }

        // GET: MembershipCategories/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("MembershipCategoryNumber,MembershipCategoryDescription,MembershipCategoryTotalLoans")] MembershipCategory membershipCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipCategory);
        }

        // GET: MembershipCategories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategory = await _context.MembershipCategories.FindAsync(id);
            if (membershipCategory == null)
            {
                return NotFound();
            }
            return View(membershipCategory);
        }

        // POST: MembershipCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipCategoryNumber,MembershipCategoryDescription,MembershipCategoryTotalLoans")] MembershipCategory membershipCategory)
        {
            if (id != membershipCategory.MembershipCategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipCategoryExists(membershipCategory.MembershipCategoryNumber))
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
            return View(membershipCategory);
        }

        // GET: MembershipCategories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategory = await _context.MembershipCategories
                .FirstOrDefaultAsync(m => m.MembershipCategoryNumber == id);
            if (membershipCategory == null)
            {
                return NotFound();
            }

            return View(membershipCategory);
        }

        // POST: MembershipCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipCategory = await _context.MembershipCategories.FindAsync(id);
            _context.MembershipCategories.Remove(membershipCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipCategoryExists(int id)
        {
            return _context.MembershipCategories.Any(e => e.MembershipCategoryNumber == id);
        }
    }
}
