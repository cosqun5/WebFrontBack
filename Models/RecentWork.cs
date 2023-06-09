﻿

using System.ComponentModel.DataAnnotations;

namespace WebFrontToBack.Models
{
	public class RecentWork
	{
        public int Id { get; set; }

		[Required(ErrorMessage = "Bos ola bilmez"), MaxLength(50, ErrorMessage = "Uzunlq maximum 50 simvol olmalidir")]
		public string Title { get; set; }


		[Required]
		public string Description { get; set; }

		[Required]
		public string Path { get; set; }

	}
}
