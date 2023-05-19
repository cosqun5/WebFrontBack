using Microsoft.Build.Framework;

namespace WebFrontToBack.Models;

public class Service
{
    public Service()
    {
        ServiceImages = new List<ServiceImage>();
    }
    public int Id { get; set; }


    [Required]
    public string? Name { get; set; }

	[Required]
	public string? Description { get; set; }

	[Required]
	public bool IsDeleted { get; set; }

	[Required]
	public double? Price { get; set; }

	[Required]
	public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public virtual List<ServiceImage> ServiceImages { get; set; }
}
