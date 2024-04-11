using AutoMapper;
using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Enums;
using Films.Models;
using Films.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Films.Controllers
{
    public class FilmController : Controller
    {            
        private readonly FilmsService _service;

        public FilmController(AppDbContext context)
        {          
            _service = new FilmsService(context);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] SortType? sortType, [FromQuery] int[]? categories, string? director, [FromServices] IMapper mapper)
        {
           var films = await _service.GetAllFilmsAsync(categories, director);

            var filmModels = mapper.Map<List<FilmModel>>(films);

            var listModel = new FilmsListModel
            {
                Films = filmModels,
                FilmsFilter = new FilmsFilterModel { Categories = categories, Director = director }
            };

            return View(listModel);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] EditFIlmRequestModel request, [FromServices] IMapper mapper)
        {

            var film = mapper.Map<Film>(request);

            await _service.AddFilm(film, request.Categories);

            return RedirectToAction("List");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromServices]IMapper mapper)
        {
            var film = await _service.GetFilmById(id);

            if (film == null)           
                return NotFound();
          
            var editFilmModel = mapper.Map<EditFilmModel>(film);
          
            return View(editFilmModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditFIlmRequestModel request, [FromServices] IMapper mapper)
        {            
            var film = mapper.Map<Film>(request);

            await _service.UpdateFilm(film, request.Categories);

            return RedirectToAction("List");                    
        }
    }
}
