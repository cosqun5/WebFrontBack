using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFrontToBack.ViewModel;

namespace WebFrontToBack.Controllers
{
    public class BasketController : Controller
    {
        private const string COOKIES_BASKET = "basketVM";
        public IActionResult Index()
        {
            List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies[COOKIES_BASKET]);
            return Json(basketVMs);
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
