using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Enums;
using Microsoft.EntityFrameworkCore;

namespace Films.Services
{
    public class FilmsService
    {
        private readonly AppDbContext _context;       
        private readonly DbSet<Film> _filmsDbSet;

        public FilmsService(AppDbContext context) 
        {
            _context = context;
            _filmsDbSet = context.Films;
        }

        public async Task UpdateFilm(Film film, int[] categories)
        {
            var record = await _filmsDbSet.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == film.Id);

            if (record == null)
                throw new Exception("Wrong film id");
           
            record.Director = film.Director;
            record.Name = film.Name;
       
            record.Categories.Clear();
            record.Categories = await _context.Categories.Where(c => categories.Contains(c.Id)).ToListAsync();

            await _context.SaveChangesAsync();            
        }

        public async Task<List<Film>> GetAllFilmsAsync(int[]? categories = null, string? director = null, SortType? sortType = null)
        {
            var films = _filmsDbSet.AsQueryable();
            if (categories!.Any())           
                films = films.Where(x => x.Categories.Any(y => categories!.Contains(y.Id)));
            if (!string.IsNullOrWhiteSpace(director))
                films = films.Where(x => x.Director.Contains(director));

            var result = await films.Include(x => x.Categories)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task <Film?> GetFilmById(int id)
        {
            var film = await _filmsDbSet
              .Include(x => x.Categories)
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == id);

            return film;
        }

        public async Task AddFilm(Film film, int[] categories)
        {
            await _filmsDbSet.AddAsync(film);
            film.Categories = await _context.Categories.Where(c => categories.Contains(c.Id)).ToListAsync();

            await _context.SaveChangesAsync();
        }
    }
}
