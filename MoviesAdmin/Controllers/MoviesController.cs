
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;

    public MoviesController(MoviesAdminContext context)
    {
        _context = context;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index(string? sortOrder)
    {
        // each header link sends the opposite direction of the current sort
        ViewData["ReleasedSort"] = (sortOrder == null || sortOrder == "releasedDesc") ? "releasedAsc" : "releasedDesc"; // If condition to make it so it will swap between ascending or descening 
        ViewData["RuntimeSort"] = sortOrder == "runtimeAsc" ? "runtimeDesc" : "runtimeAsc"; // Same as above but for runtime instead
        ViewData["TitleSort"] = sortOrder == "titleAsc" ? "titleDesc" : "titleAsc"; // A-Z ascending descending

        var movies = _context.Movie.AsQueryable();

        movies = sortOrder switch // switch statement for the text to the actual order code
        {
            "releasedAsc" => movies.OrderBy(m => m.Released),
            "runtimeAsc" => movies.OrderBy(m => m.Runtime),
            "runtimeDesc" => movies.OrderByDescending(m => m.Runtime),
            "titleAsc" => movies.OrderBy(m => m.Title),
            "titleDesc" => movies.OrderByDescending(m => m.Title),
            _ => movies.OrderByDescending(m => m.Released) // defaults to the newest by catching the null when the page is first opened
        };

        return View(await movies.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,AgeRating,Genre,Runtime,Released,ImageFileName,Director")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Description,AgeRating,Genre,Runtime,Released,ImageFileName,Director")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
