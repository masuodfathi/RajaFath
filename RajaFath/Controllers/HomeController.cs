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



            var viewModel = new HomePageViewModel
            {
                Projects = await GetCategoryPostsAsync("پروژه"),
                News = await GetCategoryPostsAsync("خبر"),
                Activities = await GetCategoryPostsAsync("زمینه فعالیت"),
                posts = await GetCategoryPostsAsync("مقاله"),
                Carousel = slides
            };

            return View(viewModel);
        }
        private async Task<List<homePostViewModel>> GetCategoryPostsAsync(string categoryName)
        {
            var postList = await _context.Posts
                .Where(p => p.PostCategory.Any(pc => pc.Category.Name == categoryName))
                .OrderByDescending(p => p.CreatedDate)
                .Take(3)
                .Include(p=>p.FeaturedImage)
                .Include(p=>p.Author)
                .ToListAsync();

            return postList.Select(post => new homePostViewModel
            {
                // Map properties from Post to homePostViewModel here
                // For example:
                Id = post.Id,
                Title = post.Title,
                Excerpt = post.Excerpt,
                Author = post.Author?.UserName,
                ImageUrl = post.FeaturedImage?.ImageUrl,
                CreatedDate = CalculateTimeDifference(post.CreatedDate),
                TimeDifference = GetFormattedTimeDifference(post.CreatedDate)



            }).ToList();
        }

        private DateTime? CalculateTimeDifference(DateTime? createdDate)
        {
            if (createdDate.HasValue)
            {
                DateTime now = DateTime.Now;
                DateTime past = createdDate.Value;
                DateTime result = now.AddTicks(-past.Ticks); // Subtract past from now
                
                string diff = GetFormattedTimeDifference(createdDate);
                return result;
            }
            return null; // Return null if the createdDate is null
        }
        private string GetFormattedTimeDifference(DateTime? createdDate)
        {
            if (createdDate.HasValue)
            {
                TimeSpan timeDifference = (TimeSpan)(DateTime.Now - createdDate);

                if (timeDifference.TotalSeconds < 60)
                {
                    return $"{(int)timeDifference.TotalSeconds} ثانیه";
                }
                else if (timeDifference.TotalMinutes < 60)
                {
                    return $"{(int)timeDifference.TotalMinutes} دقیقه";
                }
                else if (timeDifference.TotalHours < 24)
                {
                    return $"{(int)timeDifference.TotalHours} ساعت";
                }
                else if (timeDifference.TotalDays < 7)
                {
                    return $"{(int)timeDifference.TotalDays} روز";
                }
                else if (timeDifference.TotalDays < 30)
                {
                    return $"{(int)(timeDifference.TotalDays / 7)} هفته";
                }
                else if (timeDifference.TotalDays < 365)
                {
                    return $"{(int)(timeDifference.TotalDays / 30)} ماه";
                }
                else
                {
                    return $"{(int)(timeDifference.TotalDays / 365)} سال";
                }
            }
            return string.Empty;
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