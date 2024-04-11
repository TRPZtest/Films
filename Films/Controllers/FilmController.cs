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
        private readonly IMapper _mapper;

        public FilmController(FilmsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] SortType? sortType, [FromQuery] int[]? categories, string? director)
        {
           var films = await _service.GetAllAsync(categories, director);

            var filmModels = _mapper.Map<List<FilmModel>>(films);

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
        public async Task<IActionResult> Add([FromForm] EditFIlmRequestModel request)
        {

            var film = _mapper.Map<Film>(request);

            await _service.AddAsync(film, request.Categories);

            return RedirectToAction("List");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var film = await _service.GetByIdAsync(id);

            if (film == null)           
                return NotFound();
          
            var editFilmModel = _mapper.Map<EditFilmModel>(film);
          
            return View(editFilmModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditFIlmRequestModel request)
        {            
            var film = _mapper.Map<Film>(request);

            await _service.Update(film, request.Categories);

            return RedirectToAction("List");                    
        }
    }
}
