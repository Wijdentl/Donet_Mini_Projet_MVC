using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Donet_Mini_Projet.Models;
using Donet_Mini_Projet.context;
using Microsoft.AspNetCore.Authorization;

namespace Donet_Mini_Projet.Controllers
{
    public class ReclamationController : Controller
    {
        private readonly AppDbContext _context;

        public ReclamationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reclamation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reclamations.Include(r => r.Article).Include(r => r.Client);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reclamation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }
        [Authorize(Roles = "User")]
        // GET: Reclamation/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email");
            return View();
        }

        // POST: Reclamation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ArticleId,Description,Statut,DateSoumission")] Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", reclamation.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", reclamation.ClientId);
            return View(reclamation);
        }

        // GET: Reclamation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", reclamation.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", reclamation.ClientId);
            return View(reclamation);
        }

        // POST: Reclamation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ArticleId,Description,Statut,DateSoumission")] Reclamation reclamation)
        {
            if (id != reclamation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReclamationExists(reclamation.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", reclamation.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email", reclamation.ClientId);
            return View(reclamation);
        }
        [Authorize(Roles = "User")]
        // GET: Reclamation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }
        [Authorize(Roles = "User")]
        // POST: Reclamation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation != null)
            {
                _context.Reclamations.Remove(reclamation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamationExists(int id)
        {
            return _context.Reclamations.Any(e => e.Id == id);
        }
    }
}
