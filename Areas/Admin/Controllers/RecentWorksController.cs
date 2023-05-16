using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RecentWorksController : Controller
	{
		private readonly AppDbContext _context;
		public RecentWorksController(AppDbContext context)
		{
			_context = context;
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
		public async Task<IActionResult> Create(RecentWork work)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			await _context.AddAsync(work);
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
		public IActionResult Delete(int Id)
		{

			RecentWork? work = _context.RecentWorks.Find(Id);
			if (work == null)
			{
				return NotFound();
			}
			_context.RecentWorks.Remove(work);
			_context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
