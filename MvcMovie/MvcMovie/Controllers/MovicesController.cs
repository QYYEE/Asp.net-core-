using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class MovicesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MovicesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movices
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movice
                                            orderby m.Genre
                                            select m.Genre;
            var movies = from m in _context.Movice select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movices = await movies.ToListAsync()
            };

            return View(movieGenreVM);

        }
        // GET: Movices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movice = await _context.Movice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movice == null)
            {
                return NotFound();
            }

            return View(movice);
        }

        // GET: Movices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movice movice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movice);
        }

        // GET: Movices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movice = await _context.Movice.FindAsync(id);
            if (movice == null)
            {
                return NotFound();
            }
            return View(movice);
        }

        // POST: Movices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movice movice)
        {
            if (id != movice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviceExists(movice.Id))
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
            return View(movice);
        }

        // GET: Movices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movice = await _context.Movice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movice == null)
            {
                return NotFound();
            }

            return View(movice);
        }

        // POST: Movices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movice = await _context.Movice.FindAsync(id);
            _context.Movice.Remove(movice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviceExists(int id)
        {
            return _context.Movice.Any(e => e.Id == id);
        }
    }
}
