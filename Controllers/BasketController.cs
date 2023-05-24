using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebFrontToBack.DAL;
using WebFrontToBack.ViewModel;

namespace WebFrontToBack.Controllers
{
    public class BasketController : Controller
    {
        private const string COOKIES_BASKET = "basketVM";
        private readonly AppDbContext _appDbContext;
        public BasketController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
			List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
			List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies[COOKIES_BASKET]);
			foreach (BasketVM item in basketVMs)
			{
				BasketItemVM basketItemVM = _appDbContext.Services
												.Where(s => !s.IsDeleted && s.Id == item.ServiceId)
												.Include(s => s.Category)
												.Include(s => s.ServiceImages)
												.Select(s => new BasketItemVM
												{
													Name = s.Name,
													Id = s.Id,
													CategoryName = s.Category.Name,
													IsDeleted = s.IsDeleted,
													Price = s.Price,
													ServiceCount = item.Count,
													ImagePath = s.ServiceImages.FirstOrDefault(i => i.IsActive).Path
												}).FirstOrDefault();
				basketItemVMs.Add(basketItemVM);
				//List<BasketItemVM> basketItemVMs = new List<BasketItemVM>();
				//List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies[COOKIES_BASKET]);
				//foreach (BasketVM item in basketVMs)
				//{
				//    BasketItemVM basketItemVM = _appDbContext.Services
				//     .Where(s => !s.IsDeleted && s.Id == item.ServiceId)
				//     .Include(s => s.Category)
				//     .Include(s => s.ServiceImages)
				//     .Select(s => new BasketItemVM
				//     {
				//         Name = s.Name,
				//         Id = s.Id,
				//         CategoryName = s.Category.Name,
				//         IsDeleted = s.IsDeleted,
				//         Price = s.Price,
				//         ServiceCount = item.Count,
				//         ImagePath = s.ServiceImages.FirstOrDefault(i => i.IsActive).Path
				//     }).FirstOrDefault();
				//    basketItemVMs.Add(basketItemVM);



			}
            return View(basketItemVMs);
        }
        public IActionResult AddBasket(int id)
        {
            List<BasketVM> basket;
            if (Request.Cookies[COOKIES_BASKET] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies[COOKIES_BASKET]);
            }
            else
            {
                basket = new List<BasketVM> { };
            }
            BasketVM cookiesBasket = basket.Where(s => s.ServiceId == id).FirstOrDefault();
            if (cookiesBasket != null)
            {
                cookiesBasket.Count++;
            }
            else
            {

                BasketVM basketVM = new BasketVM()
                {
                    ServiceId = id,
                    Count = 1
                };
                basket.Add(basketVM);
            }

            Response.Cookies.Append(COOKIES_BASKET, JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index", "Home");
        }
    }
}
