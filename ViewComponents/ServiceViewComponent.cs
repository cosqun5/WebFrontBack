using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public ServiceViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Service> service = await _appDbContext.Services
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Take(8)
                .Include(s => s.Category)
                .Include(s => s.ServiceImages)
                .ToListAsync();
            return View(service);
        }
    }
}
