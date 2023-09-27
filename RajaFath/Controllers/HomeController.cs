using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Models;
using System.Diagnostics;

namespace RajaFath.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();
            var slides = new List<Carousel>()
            {
                new Carousel{CarouselId= 1, Title="sile1",Description="the first description",Path="images\\slide1.jpg"},
                new Carousel{CarouselId= 2, Title="carosel1",Description="the first description",Path="images\\slide2.jpg"},
                new Carousel{CarouselId= 3, Title="carosel1",Description="the first description",Path="images\\slide3.jpg"},
            };
            if (slides.Count > 0)
            {
                model.Carousel = slides;
            }
            int? IdProject = _context.Categories.Where(c => c.Name.Equals("پروژه")).FirstOrDefault().CategoryId;
            var postsInCategory = await _context.Posts
            .Where(p => p.PostCategory.Any(pc => pc.CategoryId == IdProject))
            .ToListAsync();
            if (IdProject != null)
            {
                model.post = postsInCategory;
            }
            
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}