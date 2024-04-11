using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class EditCategoryModel
    {
        [Required]
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }
        [ForeignKey(nameof(Category))]
        [Display(Name = "Parent category")]
        public int? ParentCategoryId { get; set; }
        public List<Category> AllCategories { get; set; } 
    }
}
