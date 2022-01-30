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
    public class GenreController : Controller
    {
        private readonly MediiProgramareEntityContext _context;

        public GenreController(MediiProgramareEntityContext context)
        {
            _context = context;
        }

        // GET: Genre
        public async Task<IActionResult> Index()
        {
            return View(await _context.GenreModel.ToListAsync());
        }

        // GET: Genre/Details/{Id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await _context.GenreModel
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }

        // GET: Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed([Bind("GenreId,Name,Description")] GenreModel genreModel)
        {
            if (ModelState.IsValid)
            {
                GenreModel last = _context.GenreModel.OrderByDescending(g => g.GenreId).FirstOrDefault();
                System.Diagnostics.Debug.Write(last.GenreId);

                genreModel.GenreId = last.GenreId + 1;
                _context.Add(genreModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genreModel);
        }

        // GET: Genre/Edit/{Id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await _context.GenreModel.FindAsync(id);
            if (genreModel == null)
            {
                return NotFound();
            }
            return View(genreModel);
        }

        // POST: Genre/Edit/{Id}
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("GenreId,Name,Description")] GenreModel genreModel)
        {
            if (id != genreModel.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genreModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreModelExists(genreModel.GenreId))
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
            return View(genreModel);
        }

        // GET: Genre/Delete/{Id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreModel = await _context.GenreModel
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }

        // POST: Genre/Delete/{Id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreModel = await _context.GenreModel.FindAsync(id);
            _context.GenreModel.Remove(genreModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreModelExists(int id)
        {
            return _context.GenreModel.Any(e => e.GenreId == id);
        }
    }
}
