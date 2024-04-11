using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Category> Categories { get; set; } = [];
        public string CategoriesStr { get { return string.Join(", ", Categories.Select(x => x.Name).ToArray()).Trim(','); } }
        public Dictionary<int, Category> CategoriesByParentId { get { return Categories.ToDictionary(x => x.Id, x => x);  } }
    }
}
