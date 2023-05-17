using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrontToBack.Areas.Admin.RecentWorkViewModels
{
	public class CreateRecentWorkVM
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Bos ola bilmez"), MaxLength(50, ErrorMessage = "Uzunlq maximum 50 simvol olmalidir")]
		public string Title { get; set; }


		[Required]
		public string Description { get; set; }

		[Required, NotMapped]
		public IFormFile Photo { get; set; }
	}
}
