namespace WebFrontToBack.Models
{
    public class WorkCategory
    {
        public WorkCategory()
        {
            Work work = new Work();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Work> works { get; set; }
    }
}
