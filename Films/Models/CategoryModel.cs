using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class CategoryModel
    {            
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }     
        public int? ParentCategoryId { get; set; }            
        public bool HasParent { get { return ParentCategoryId != null; } }      
    }
}
