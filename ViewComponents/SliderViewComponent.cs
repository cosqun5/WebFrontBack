using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.ViewComponents
{

    public class SliderViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public SliderViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider> sliders = await _appDbContext.Sliders.ToListAsync();
            return View(sliders);
             
                
        }
    }
}
