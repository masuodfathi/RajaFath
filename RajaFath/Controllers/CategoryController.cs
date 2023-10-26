using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Models;

namespace RajaFath.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var postList = await _context.Posts
                .Where(p => p.PostType == "post")
                .OrderByDescending(p => p.CreatedDate)
                .Include(p => p.FeaturedImage)
                .Include(p => p.Author)
                .ToListAsync();
            var postmodel = postList.Select(post => new homePostViewModel
            {
                // Map properties from Post to homePostViewModel here
                // For example:
                Id = post.Id,
                Title = post.Title,
                Excerpt = post.Excerpt,
                Author = post.Author?.UserName,
                ImageUrl = post.FeaturedImage?.ImageUrl,
                CreatedDate =post.CreatedDate,
            }).ToList();
            return View(postmodel);
        }
        public async Task<IActionResult> Category(string? Category)
        {
            ViewBag.Category = Category;
            var postList = await _context.Posts
                .Where(p => p.PostCategory.Any(pc => pc.Category.Name == Category) && p.PostType == "post")
                .OrderByDescending(p => p.CreatedDate)
                .Include(p => p.FeaturedImage)
                .Include(p => p.Author)
                .ToListAsync();
            var postmodel = postList.Select(post => new homePostViewModel
            {
                // Map properties from Post to homePostViewModel here
                // For example:
                Id = post.Id,
                Title = post.Title,
                Excerpt = post.Excerpt,
                Author = post.Author?.UserName,
                ImageUrl = post.FeaturedImage?.ImageUrl,
                CreatedDate = post.CreatedDate,
            }).ToList();
            if (postmodel.Count() == 0)
            {
                return NotFound();
            }
            return View(postmodel);
        }
    }
}
