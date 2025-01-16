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
    [Authorize(Roles= "Admin")]
    public class InterventionController : Controller
    {
        private readonly AppDbContext _context;

        public InterventionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Intervention
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Interventions.Include(i => i.Reclamation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Intervention/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.Reclamation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Intervention/Create
        public IActionResult Create()
        {
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id");
            return View();
        }

        // POST: Intervention/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReclamationId,DateIntervention,CoutMainDOeuvre,Statut")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // GET: Intervention/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // POST: Intervention/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReclamationId,DateIntervention,CoutMainDOeuvre,Statut")] Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.Id))
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
            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "Id", "Id", intervention.ReclamationId);
            return View(intervention);
        }

        // GET: Intervention/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.Reclamation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Intervention/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.Id == id);
        }
    }
}
