using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.ViewModel;

namespace WebFrontToBack.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM() { 
            RecentWorks= await _appDbContext.RecentWorks.ToListAsync(),
            Categories= await _appDbContext.Categories.Where(c=>!c.IsDeleted).ToListAsync(),
           
            };
            return View(homeVM);
        }

    }
}
