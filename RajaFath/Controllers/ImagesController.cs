using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Models;

namespace RajaFath.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //GET: Images
        public async Task<IActionResult> Index()
        {
            return _context.Images != null ?
                        View(await _context.Images.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Images'  is null.");
        }

        //// GET: Images/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Images == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //// GET: Images/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Images/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Image image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(image);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(image);
        //}

        //// GET: Images/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Images == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images.FindAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(image);
        //}

        //// POST: Images/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Image image)
        //{
        //    if (id != image.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(image);
        //}

        //// GET: Images/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Images == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //// POST: Images/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Images == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Images'  is null.");
        //    }
        //    var image = await _context.Images.FindAsync(id);
        //    if (image != null)
        //    {
        //        _context.Images.Remove(image);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult UploadForm(bool? isSuccess)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadForm(Image model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded image to a location and set the Path property
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Define a path where the image will be saved, e.g., wwwroot/images
                    var imagePath = Path.Combine("wwwroot", "images", imageFile.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = "/images/" + imageFile.FileName; // Set the image path
                    
                    
                }

                // Save the model to the database
                _context.Images.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(UploadForm),new {isSuccess=true});
            }

            return RedirectToAction("UploadForm",model);
        }

        //private bool ImageExists(int id)
        //{
        //  return (_context.Images?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
