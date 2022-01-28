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
    public class MovieController : Controller
    {
        private readonly MediiProgramareEntityContext _context;

        public MovieController(MediiProgramareEntityContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var mediiProgramareEntityContext = _context.MovieModel.Include(m => m.Genre).Include(m => m.Studio);  
            return View(await mediiProgramareEntityContext.ToListAsync());
        }

        // GET: Movie/Details/{Id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        private void PopulateGenresList(object selectedGenre = null)
        {
            var genreQuery = from g in _context.GenreModel
                                   orderby g.Name
                                   select g;
            ViewBag.GenreId = new SelectList(genreQuery.AsNoTracking(), "GenreId", "Name", selectedGenre);
        }
        private void PopulateStudiosList(object selectedStudio = null)
        {
            var studioQuery = from s in _context.StudioModel
                             orderby s.Name
                             select s;
            ViewBag.StudioId = new SelectList(studioQuery.AsNoTracking(), "StudioId", "Name", selectedStudio);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            PopulateGenresList();
            PopulateStudiosList();
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Name,Rank,Peak,WorldBoxOffice,Year,GenreId,StudioId")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            { 
                MovieModel last = _context.MovieModel.OrderByDescending(m => m.MovieId).FirstOrDefault();

                _context.Entry(movieModel.Genre).State = EntityState.Unchanged;
                _context.Entry(movieModel.Studio).State = EntityState.Unchanged;

                movieModel.MovieId = last.MovieId + 1;

                movieModel.Genre = null;
                movieModel.Studio = null;

                _context.Add(movieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateGenresList(movieModel.GenreId);
            PopulateStudiosList(movieModel.StudioId);
            return View(movieModel);
        }

        // GET: Movie/Edit/{Id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel.FindAsync(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            PopulateGenresList();
            PopulateStudiosList();
            return View(movieModel);
        }

        // POST: Movie/Edit/{Id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Name,Rank,Peak,WorldBoxOffice,Year,GenreId,StudioId")] MovieModel movieModel)
        {
            if (id != movieModel.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(movieModel.Genre).State = EntityState.Unchanged;
                    _context.Entry(movieModel.Studio).State = EntityState.Unchanged;

                    movieModel.Genre = null;
                    movieModel.Studio = null;
                    _context.Update(movieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelExists(movieModel.MovieId))
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
            PopulateGenresList(movieModel.GenreId);
            PopulateStudiosList(movieModel.StudioId);
            return View(movieModel);
        }

        // GET: Movie/Delete/{Id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // POST: Movie/Delete/{Id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieModel = await _context.MovieModel.FindAsync(id);
            _context.MovieModel.Remove(movieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelExists(int id)
        {
            return _context.MovieModel.Any(e => e.MovieId == id);
        }
    }
}
