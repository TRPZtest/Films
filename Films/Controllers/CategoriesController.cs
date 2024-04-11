using AutoMapper;
using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Models;
using Films.Services;
using Microsoft.AspNetCore.Mvc;

namespace Films.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoriesService _service;
        private readonly IMapper _mapper;

        public CategoriesController(CategoriesService service, IMapper mapper) 
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _service.GetCategoriesList();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int Id)
        {
            var category = await _service.GetById(Id);

            if (category == null)             
                return NotFound();
            
            var allCategories = await _service.GetAllAsync();
            allCategories.RemoveAll(x => x.Id == category.Id);

            var editModel = _mapper.Map<EditCategoryModel> (category);

            editModel.AllCategories = allCategories;

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditCategoryModel request)
        {
            if (request.Id == request.ParentCategoryId)
                return BadRequest();

            var allCategories = await _service.GetAllAsync();

            var category = _mapper.Map<Category>(request);

            await _service.Edit(category);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var allCategories = await _service.GetAllAsync();

            var editModel = new EditCategoryModel { AllCategories = allCategories };

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] EditCategoryModel request)
        {
            var category = _mapper.Map<Category>(request);

            await _service.Add(category);
            return RedirectToAction("List");
        }
    }
}
