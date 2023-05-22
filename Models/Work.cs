namespace WebFrontToBack.Models
{
    public class Work
    {
        public Work()
        {
            WorkImage workImage = new WorkImage();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int WorkCategoryId { get; set; }
        public WorkCategory? WorkCategories { get; set; }
        public List<WorkImage>? WorkImages { get; set; } 
    }
}
