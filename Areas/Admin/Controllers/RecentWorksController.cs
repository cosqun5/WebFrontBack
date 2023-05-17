using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.Areas.Admin.RecentWorkViewModels;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.Utilities.Extensions;

namespace WebFrontToBack.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RecentWorksController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public RecentWorksController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			ICollection<RecentWork> works = await _context.RecentWorks.ToListAsync();
			return View(works);
		}
		public IActionResult Create()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateRecentWorkVM work)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}
			if (!work.Photo.CheckContentType("image/"))
			{
				ModelState.AddModelError("Photo", $"{work.Photo.FileName} - must be image type");
			}
			if (!work.Photo.CheckFileSize(200))
			{
				ModelState.AddModelError("Photo", $"{work.Photo.FileName} - file must be image size 200kb");

			}
			string root = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");

			string FileName = await work.Photo.SaveAsync(root);
			RecentWork recentWork = new RecentWork()
			{
				Title = work.Title,
				Description = work.Description,
				Path = FileName
			};

			await _context.AddAsync(recentWork);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int Id)
		{
			RecentWork? work = _context.RecentWorks.Find(Id);
			if (work == null)
			{
				return NotFound();
			}
			return View(work);

		}
		[HttpPost]
		public IActionResult Edit(RecentWork work)
		{
			RecentWork? recentWork = _context.RecentWorks.Find(work.Id);
			if (recentWork == null)
			{
				return NotFound(nameof(RecentWork));
			}
			recentWork.Title = work.Title;
			recentWork.Description = work.Description;
			recentWork.Path = work.Path;
			_context.RecentWorks.Update(recentWork);
			_context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int Id)
		{
			RecentWork work = _context.RecentWorks.Find(Id);
			if (work == null) return NotFound();
			string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", work.Path);
			if (System.IO.File.Exists(imagePath))
			{
				System.IO.File.Delete(imagePath);
			}
			_context.RecentWorks.Remove(work);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		//public IActionResult Delete(int Id)
		//{

		//	RecentWork? work = _context.RecentWorks.Find(Id);
		//	if (work == null)
		//	{
		//		return NotFound();
		//	}
		//	_context.RecentWorks.Remove(work);
		//	_context.SaveChangesAsync();
		//	return RedirectToAction("Index");
		//}
	}
}
