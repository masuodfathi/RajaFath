using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Models;

namespace RajaFath.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Show(int id)
        {
            var posts = await _context.Posts.Where(p => p.Id == id)
                    .Include(p => p.FeaturedImage)
                    .Include(p => p.PostCategory)
                    .Include(p => p.Author)
                    .FirstOrDefaultAsync();
            if (posts != null)
            {
                var model = new PageViewModel
                {
                    Id = posts.Id,
                    Title = posts.Title,
                    Content = posts.Content,
                    UpdateDate = posts.UpdatedDate,
                    Author = posts.Author,
                    FeaturImage = posts.FeaturedImage
                };
                return View(model);
            }
            return NotFound();
        }
    }
}
