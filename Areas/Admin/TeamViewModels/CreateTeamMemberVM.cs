using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebFrontToBack.Areas.Admin.TeamViewModels
{
	public class CreateTeamMemberVM
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "Bos ola bilmez"), MaxLength(50, ErrorMessage = "Uzunlq maximum 50 simvol olmalidir")]
		public string FulName { get; set; }

		[Required]
		public string Profection { get; set; }


		[Required, NotMapped]
		public IFormFile Photo { get; set; }
	}
}
