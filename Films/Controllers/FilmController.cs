using Films.Data.Db;
using Films.Data.Db.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Films.Controllers
{
    public class FilmController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Film> _filmsDbSet;

        public FilmController(AppDbContext context)
        {
            _context = context;
            _filmsDbSet = context.Films;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var films = await _filmsDbSet.ToListAsync();

           return View(films);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute]int id)
        {
            var film = await _filmsDbSet.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
    }
}
