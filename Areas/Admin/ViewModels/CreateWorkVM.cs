using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.ViewModels
{
    public class CreateWorkVM
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int WorkCategoryId { get; set; }
        public List<WorkCategory>? WorkCategories { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
