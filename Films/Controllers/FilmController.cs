using AutoMapper;
using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Models;
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
        public async Task<IActionResult> Details([FromRoute]int id, [FromServices]IMapper mapper)
        {
            var film = await _filmsDbSet
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == id);

            var allcategories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            if (film == null)
            {
                return NotFound();
            }

            var editFilmModel = mapper.Map<EditFilmModel>(film);
            editFilmModel.AllCategories = allcategories;

            return View(editFilmModel);
        }
    }
}
