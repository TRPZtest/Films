using AutoMapper;
using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Films.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<List<CategoryModel>> CategoriesByFilmId([FromQuery]int filmId, [FromServices] IMapper mapper)
        {
            var film = await _context.Films
                .Include(x => x.Categories)
                .AsNoTracking()
                .FirstAsync(x => x.Id == filmId);

            var childCategories = film.Categories;

            var childCategoryModels = mapper.Map<List<CategoryModel>>(childCategories);

            return childCategoryModels;
        }

        [HttpGet]
        public async Task<List<CategoryModel>> Categories([FromServices] IMapper mapper)
        {
            var childCategories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            var childCategoryModels = mapper.Map<List<CategoryModel>>(childCategories);

            return childCategoryModels;
        }

        [HttpGet]
        public async Task<List<CategoryModel>> ChildCategories([FromQuery]int id, [FromServices] IMapper mapper)
        {
            var childCategories = await _context.Categories
                .AsNoTracking()
                .Where(x => x.ParentCategoryId == id)
                .ToListAsync();

            var childCategoryModels = mapper.Map<List<CategoryModel>>(childCategories);

            return childCategoryModels;
        }             
    }
}
