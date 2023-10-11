using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

            //if (IdProject != null)
            //{

            //    foreach (var p in postsInCategory)
            //    {
            //        var newPost = new homePostViewModel();
            //        newPost.Id = p.Id;
            //        newPost.Title = p.Title;
            //        newPost.Excerpt = p.Excerpt;
            //        newPost.Category = "پروژه";
            //        newPost.Author = GetUserOfThePost(p.Id);
            //        newPost.ImageUrl = GetImageOfThePost(p.FeaturedImageId);
            //        model.Projects.Append(newPost);
            //    }

            //}
            var postsWithImages = _context.Posts
                .Where(post => post.PostCategory.Any(pc => pc.CategoryId == IdProject))
                .OrderByDescending(p => p.CreatedDate)
                .Take(3)
                .Include(p => p.FeaturedImage) // Eager load the related image
                .ToList();
            if (postsWithImages != null)
            {
                model.Projects = postsWithImages;
            }
            return View(model);
        }
        private string GetImageOfThePost(int? Id)
        {
            if (Id == null)
            {
                return string.Empty;
            }
            var image = _context.Images.FindAsync(Id).Result;
            if (image == null)
            {
                return string.Empty;
            }
            return image.ImageUrl;
        }
        private string GetUserOfThePost(int? Id)
        {
            if (Id == null)
            {
                return string.Empty;
            }
            var user = _context.Users.FindAsync(Id.ToString()).Result;
            if (user == null)
            {
                return string.Empty;
            }
            string username = user.UserName;
            return username;    
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}