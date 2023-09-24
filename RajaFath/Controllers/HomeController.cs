using Microsoft.AspNetCore.Mvc;
using RajaFath.Models;
using System.Diagnostics;

namespace RajaFath.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var slides = new List<Carousel>()
            {
                new Carousel{Id= "1", Title="carosel1",Description="the first description",Path="images\\slid1.jpg"},
                new Carousel{Id= "2", Title="carosel1",Description="the first description",Path="images\\slid2.jpg"},
                new Carousel{Id= "3", Title="carosel1",Description="the first description",Path="images\\slid3.jpg"},
            };
            return View(slides);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}