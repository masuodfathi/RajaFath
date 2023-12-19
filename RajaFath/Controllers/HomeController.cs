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
            var setting = await _context.siteSettings.FirstOrDefaultAsync();
            var model = new HomePageViewModel();
            var projects = await GetCategoryPostsAsync("پروژه");
            var slides = new List<Carousel>();
            foreach (var item in projects)
            {
                var carousel = new Carousel()
                {
                    CarouselId = item.Id,
                    Title = item.Title,
                    Description = item.Excerpt,
                    Path = item.ImageUrl,
                };
                slides.Add(carousel);
            }
            if (slides.Count > 0)
            {
                model.Carousel = slides;
            }

            var viewModel = new HomePageViewModel
            {
                Projects = projects,
                News = await GetCategoryPostsAsync("خبر",4),
                Activities = await GetCategoryPostsAsync("زمینه فعالیت"),
                posts = await GetCategoryPostsAsync("مقاله"),
                Carousel = slides,
                siteSetting = setting
            };

            return View(viewModel);
        }

        

        private async Task<List<homePostViewModel>> GetCategoryPostsAsync(string categoryName,int number=3)
        {
            if ( _context.Posts != null)
            {
                var postList = await _context.Posts
                .Where(p => p.PostCategory.Any(pc => pc.Category.Name == categoryName) && p.PostType == "post")
                .OrderByDescending(p => p.CreatedDate)
                .Take(number)
                .Include(p => p.FeaturedImage)
                .Include(p => p.Author)
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
            else
            {
                return null;
            }
            

            
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
        public async Task<IActionResult> Managers()
        {
            var managers = await _context.Posts
                .Where(p => p.PostCategory.Any(p => p.Category.Name == "اعضا") && p.PostType == "page")
                .OrderBy(p => p.CreatedDate)
                .Include(p => p.FeaturedImage)
                .ToListAsync();
            var model = managers.Select(m => new Managers
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                Excerpt = m.Excerpt,
                Image = m.FeaturedImage
            }).ToList();
            return View(model);
        }

    }
}