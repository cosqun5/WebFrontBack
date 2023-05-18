using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;

namespace WebFrontToBack.Controllers
{
	public class TeamMemberController : Controller
	{
		private readonly AppDbContext _context;
		public TeamMemberController(AppDbContext context)
		{
			_context = context;
		}
		public async Task< IActionResult> Index()
		{
			return View(await _context.TeamMembers.ToListAsync());
		}
	}
}
