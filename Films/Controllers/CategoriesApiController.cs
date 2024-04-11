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
        private readonly CategoriesService _service;
        private readonly IMapper _mapper;

        public CategoriesApiController(CategoriesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<List<CategoryModel>> CategoriesByFilmId([FromQuery]int filmId)
        {
            var categories = await _service.GetByFilmId(filmId);

            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);

            return categoryModels;
        }

        [HttpGet]
        public async Task<List<CategoryModel>> Categories()
        {
            var categories = await _service.GetAllAsync();

            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);

            return categoryModels;
        }               
    }
}
