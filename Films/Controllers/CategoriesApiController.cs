using AutoMapper;
using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Models;
using Films.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Films.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly CategoriesService _servies;     

        public CategoriesApiController(AppDbContext context)
        {
            _servies = new CategoriesService(context);          
        }

        [HttpGet()]
        public async Task<List<CategoryModel>> CategoriesByFilmId([FromQuery]int filmId, [FromServices] IMapper mapper)
        {
            var categories = await _servies.GetByFilmId(filmId);

            var categoryModels = mapper.Map<List<CategoryModel>>(categories);

            return categoryModels;
        }

        [HttpGet]
        public async Task<List<CategoryModel>> Categories([FromServices] IMapper mapper)
        {
            var categories = await _servies.GetAllAsync();

            var categoryModels = mapper.Map<List<CategoryModel>>(categories);

            return categoryModels;
        }               
    }
}
