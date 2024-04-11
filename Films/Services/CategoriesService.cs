using Films.Data.Db;
using Films.Data.Db.Entities;
using Films.Models;
using Microsoft.EntityFrameworkCore;

namespace Films.Services
{
    public class CategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Category?> GetById(int id)
        {
            var category = await _context.Categories.Include(x => x.Films).FirstOrDefaultAsync(x => x.Id == id);
        
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            return categories;
        }

        public async Task Edit(Category category)
        {
            if (category.Id == category.ParentCategoryId)
                throw new Exception("Wrong parent category Id");

            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryListItemModel>> GetCategoriesList()
        {
            var categories = await _context.Categories.Select<Category, CategoryListItemModel>(x => new CategoryListItemModel { Id = x.Id, Name = x.Name, FilmsCount = x.Films.Count() }).ToListAsync();

            foreach (var category in categories)
            {
                category.NestingLevel = await GetNestingLevel(category.Id);
            }
                               
            return categories;
        }

        public async Task<List<Category>> GetByFilmId(int filmId)
        {
            var film = await _context.Films.Include(x => x.Categories).FirstAsync(x => x.Id ==  filmId);

            return film.Categories;
        }

        private async Task<int> GetNestingLevel(int categoryId)
        {
            var category =  await _context.Categories.FindAsync(categoryId);

            int nestingLevel = 0;
            var parent = category.ParentCategory;

            while (parent != null)
            {
                parent = parent?.ParentCategory;
                nestingLevel++;
            }

            return nestingLevel;
        }
    }
}
