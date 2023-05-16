using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.ViewModels
{
	public class ServiceVM
	{
	 public	Service Services { get; set; }
	 public	List<Category>? Categories { get; set; }
	}
}
