using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Films.Data.Db.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }
        [ForeignKey(nameof(Category))]
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Film> Films { get; set; } = new List<Film>();
    }
}
