using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebFrontToBack.Areas.Admin.ViewModels;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.ViewModel;

namespace WebFrontToBack.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController : Controller
	{
		private readonly AppDbContext _context;
		public ServiceController(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			ICollection<Service> services = await _context.Services
				.Include(s => s.Category)
				.ToListAsync();
			return View(services);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			Service service = new Service();
			ServiceVM servicevm = new ServiceVM()
			{

				Categories = await _context.Categories.ToListAsync(),
				Services = service,

			};

			return View(servicevm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ServiceVM service)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			bool isExists = await _context.Categories.AllAsync(s => s.Name.ToLower() == service.Services.Name.ToLower());
			if (!isExists)
			{
				ModelState.AddModelError("Name", "Service name already exists");
				return View(service);
			}

			//bool isExists = await _context.Categories.AnyAsync(c =>
			//  c.Name.ToLower().Trim() == service.Services.Name.ToLower().Trim());
			//         //bool isExists = await _context.Categories.AnyAsync(c =>
			//         //c.Name.ToLower() == service.Services.Name.ToLower().Trim());
			//         if (!isExists)
			//         {

			//             ModelState.AddModelError("Name", "Service name already exists");
			//             return View(service);
			//         }
			await _context.Services.AddAsync(service.Services);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int Id)
		{
			Service? service = _context.Services.Find(Id);
			if (service == null)
			{
				return NotFound();
			}
			_context.Services.Remove(service);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Update(int Id)
		{
			Service? service = _context.Services.Find(Id);
			ServiceVM servicevm = new ServiceVM()
			{

				Categories = await _context.Categories.ToListAsync(),
				Services = service,

			};

			return View(servicevm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ServiceVM servicevm)
		{
			Service? newservice = await _context.Services.FindAsync(servicevm.Services.Id);

			if (newservice == null)
			{
				return NotFound();
			}
			newservice.Name = servicevm.Services.Name;
			newservice.Description = servicevm.Services.Description;
			newservice.Price = servicevm.Services.Price;
			newservice.CategoryId = servicevm.Services.CategoryId;
			_context.Services.Update(newservice);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult AddPhoto()
		{

			return View();
		}

	}
}
