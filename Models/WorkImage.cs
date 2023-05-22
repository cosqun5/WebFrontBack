namespace WebFrontToBack.Models
{
    public class WorkImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }  
        public int WorkId { get; set; }  
        public Work Work { get; set; }
    }
}
