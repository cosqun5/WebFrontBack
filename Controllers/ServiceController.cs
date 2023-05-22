using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ServiceController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult>  Index()
        {
            ViewBag.dbServiceCount = await _appDbContext.Services.CountAsync();
            return View(await _appDbContext.Services
                .Include(s => s.ServiceImages)
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Take(8)
                .ToListAsync());
        }
        public async Task<IActionResult> LoadMore(int skip=0,int take = 8)
        {
            List<Service> services = await _appDbContext.Services
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .Skip(skip)
                .Take(take)
                .Include(s => s.ServiceImages)
                .ToListAsync();
            return PartialView ("_ServicePartialView",services);
        }

    }
}
