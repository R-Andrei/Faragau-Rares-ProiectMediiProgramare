#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediiProgramareEntity.Data;
using MediiProgramareEntity.Models;

namespace MediiProgramareEntity.Controllers
{
    public class StudioController : Controller
    {
        private readonly MediiProgramareEntityContext _context;

        public StudioController(MediiProgramareEntityContext context)
        {
            _context = context;
        }

        // GET: Studio
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudioModel.ToListAsync());
        }

        // GET: Studio/Details/{Id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel
                .FirstOrDefaultAsync(m => m.StudioId == id);
            if (studioModel == null)
            {
                return NotFound();
            }

            return View(studioModel);
        }

        // GET: Studio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudioId,Name,Movies,HomeBoxOffice,WorldBoxOffice")] StudioModel studioModel)
        {
            if (ModelState.IsValid)
            {

                StudioModel last = _context.StudioModel.OrderByDescending(s => s.StudioId).FirstOrDefault();
                System.Diagnostics.Debug.Write(last.StudioId);

                studioModel.StudioId = last.StudioId + 1;
                _context.Add(studioModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studioModel);
        }

        // GET: Studio/Edit/{Id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel.FindAsync(id);
            if (studioModel == null)
            {
                return NotFound();
            }
            return View(studioModel);
        }

        // POST: Studio/Edit/{Id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudioId,Name,Movies,HomeBoxOffice,WorldBoxOffice")] StudioModel studioModel)
        {
            if (id != studioModel.StudioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studioModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioModelExists(studioModel.StudioId))
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
            return View(studioModel);
        }

        // GET: Studio/Delete/{Id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel
                .FirstOrDefaultAsync(m => m.StudioId == id);
            if (studioModel == null)
            {
                return NotFound();
            }

            return View(studioModel);
        }

        // POST: Studio/Delete/{Id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studioModel = await _context.StudioModel.FindAsync(id);
            _context.StudioModel.Remove(studioModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudioModelExists(int id)
        {
            return _context.StudioModel.Any(e => e.StudioId == id);
        }
    }
}
