using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RajaFath.Data;
using RajaFath.Models;
using System.Xml.Linq;

namespace RajaFath.Controllers
{
    public class Page : Controller
    {
        private readonly ApplicationDbContext _context;
        public Page(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> show(string slug) 
        {
            string title = slug.ToString().Replace("-", " ");
            var page = await _context.Posts.Where(p => p.Title == title && p.PostType == "page")
                    .Include(p => p.FeaturedImage)
                    .Include(p => p.PostCategory)
                    .Include(p => p.Author)
                    .FirstOrDefaultAsync();
            if (page != null)
            {
                var model = new PageViewModel
                {
                    Id = page.Id,
                    Title = page.Title,
                    Content = page.Content,
                    UpdateDate = page.UpdatedDate,
                    Author = page.Author,
                    FeaturImage = page.FeaturedImage
                };
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
