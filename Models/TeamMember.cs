using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFrontToBack.Models
{
	public class TeamMember
	{
		public int Id { get; set; }


		[Required (ErrorMessage ="Bos ola bilmez"),MaxLength (50,ErrorMessage ="Uzunlq maximum 50 simvol olmalidir")]
		public string FulName { get; set; }

		[Required]
		public string Profection { get; set; }

		[Required]
		public string Path { get; set; }



	}
}
