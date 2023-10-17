using HtmlAgilityPack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Models;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RajaFath.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, SignInManager<IdentityUser> signInManager,ApplicationDbContext contextGet)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        // List roles
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // Add a new role
        public async Task<IActionResult> AddRole(string roleName)
        {
            // Implement code to add a role
            var role = new IdentityRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                // Role creation successful
                return RedirectToAction("ListRoles");
            }
            else
            {
                // Handle errors
                return View();
            }
        }

        // Edit an existing role
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return RedirectToAction("ListRoles");
            }

            var role = await _roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                return RedirectToAction("ListUsers");
            }

            var model = new IdentityRole
            {
                Id = Id,
                Name = role.Name
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(string Id, string Name)
        {
            //Implement code to edit a role
            var role = await _roleManager.FindByIdAsync(Id);
            string oldName = role.Name;
            role.Name = Name;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                // Role update successful
                return RedirectToAction("ListRoles");
            }
            else
            {
                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // Create a view model to display user information
                var model = new IdentityRole
                {
                    Id = role.Id,
                    Name = oldName
                };

                return View(model);
            }
        }
        // Delete a role
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            // Implement code to delete a role
            var role = await _roleManager.FindByIdAsync(roleId);
            
            if (role == null)
            {
                return NotFound();
            }
            var model = new IdentityRole 
            { 
                Id = roleId,
                Name = role.Name,
            };
            var allUsers = await _userManager.GetUsersInRoleAsync(role.Name);
            ViewBag.NumberofUsersWithThisRole = allUsers.Count();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfirmed(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                
                if (result.Succeeded)
                {
                    // Role deletion successful
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    // Handle errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    // Create a view model to display user information
                    var model = new IdentityRole
                    {
                        Id = role.Id,
                        Name = role.Name
                    };
                    
                    return View(model);
                }
            }
            else
            {
                // Role not found
                return NotFound();
            }
        }

        /////////////////////////Users

        public IActionResult ListUsers()
        {
            var users = _userManager.Users.ToList();
            var usersWithRoles = new List<UserListViewModel>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userWithRole = new UserListViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() // Assuming a user can have only one role
                };
                usersWithRoles.Add(userWithRole);
            }

            return View(usersWithRoles);
        }

        public IActionResult CreateUser()
        {
            var roles = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();

            var model = new UserListViewModel
            {
                SelectListAllRoles = roles
            };

            return View(model);
        }
        private IdentityUser CreateIdentityUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserListViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user based on the user input
                var user = CreateIdentityUser();

                await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

                // Create the user with the UserManager
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign the selected role to the user
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    //Set id
                    var userId = await _userManager.GetUserIdAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // Redirect to a success page or perform other actions
                    return RedirectToAction("ListUsers");
                }

                // If user creation failed, add errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If ModelState is not valid, redisplay the form with errors
            model.SelectListAllRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ListUsers");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("ListUsers");
            }

            var roles = await _userManager.GetRolesAsync(user);
            

            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault(),
                AllRoles = _roleManager.Roles.Select(role => role.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return RedirectToAction("ListUsers");
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Update the user's roles based on the selected role
                    var rolesToRemove = await _userManager.GetRolesAsync(user);
                    var rolesToAdd = model.SelectedRole;

                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    await _userManager.AddToRoleAsync(user, rolesToAdd);

                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Populate roles again for the dropdown list
            model.AllRoles = _roleManager.Roles.Select(role => role.Name).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Retrieve user information based on the provided ID and populate the model
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var postNumber = _context.Posts.Count(p => p.AuthorId.Equals(user.Id));

            

            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PostNumber = postNumber
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            // Check if the user ID is provided
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ListUsers"); // Redirect to the user list if no user ID is provided
            }

            // Retrieve the user by ID
            var user = await _userManager.FindByIdAsync(id);

            // Check if the user exists
            if (user == null)
            {
                return RedirectToAction("ListUsers"); // Redirect to the user list if the user does not exist
            }

            // Delete the user
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // User successfully deleted
                return RedirectToAction("ListUsers"); // Redirect to the user list or a success page
            }
            else
            {
                // Handle any errors that occurred during user deletion
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // Create a view model to display user information
                var model = new UserListViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };

                return View(model);
            }
        }

        private async Task<List<UserListViewModel>> GetUsersWithRoles()
        {
            var usersWithRoles = new List<UserListViewModel>();

            // Retrieve all users
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Create a view model to store user information and roles
                var userWithRoles = new UserListViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault()
                };

                usersWithRoles.Add(userWithRoles);
            }

            return usersWithRoles;
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }


        ////////////////post/////////////////////
        public async Task<IActionResult> PostList(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1; // Get the current page number from the request, default to 1 if not provided

            
            var posts = await _context.Posts
                .Include(p => p.Author) // Load related User data
                .OrderByDescending(p => p.UpdatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var postViewModels = posts.Select(p => new PostInListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Excerpt = p.Excerpt,
                AuthorName = p.Author?.UserName, // Access the related user's username
                UpdateDate = p.UpdatedDate,
            }).ToList();

            var model = new PostList
            {
                Posts = postViewModels,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPosts = _context.Posts.Count() // Include the total count for pagination
            };


            return View(model);
            
        }
        [HttpGet]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(Id);
            if (post != null)
            {
                var model = new PostCreateViewModel
                {
                    PostId = post.Id,
                    Title = post.Title,
                    Excerpt = post.Excerpt,

                };
                return View(model);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostConfirmed(int? Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            

            var post = await _context.Posts.FindAsync(Id);

            if (post != null)
            {
                _context.Posts.Remove(post);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    return BadRequest(ex.Message);
                }
                
            }
            return RedirectToAction("PostList");
        }
        [HttpGet]
        public async Task<IActionResult> CreatePost(int? Id)
        {
            var post = await _context.Posts.FindAsync(Id);
            if (post != null)
            {
                var model = await CreatePostViewModelAsync(post);
                return View(model);
            }
            else
            {
                var model = await CreateRawModelFromModel();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(PostCreateViewModel model)
        {
            if (model.PostId.HasValue)
            {
                if (ModelState.IsValid)
                {
                    var existPost = _context.Posts.Find(model.PostId);
                    if (existPost != null)
                    {
                        // Map properties from the view model to your Post model
                        var updatedpost = MakeUpdateExistPostFromModel(model, existPost);

                        if (model.SelectedCategoryIds != null)
                            await UpdatePostCategoriesAsync(existPost.Id, model.SelectedCategoryIds);

                        _context.Posts.Update(updatedpost);
                        await _context.SaveChangesAsync();

                        ModelState.Remove("Excerpt");
                        

                        model = await CreatePostViewModelAsync(updatedpost);
                        return View(model);
                    }
                    // update model with saved post

                    return RedirectToAction("PostList");
                }
                model = await CreateRawModelFromModel(model);
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    // Map properties from the view model to your Post model
                    var post = MakeNewPostFromModel(model, currentUser.Id);

                    await _context.Posts.AddAsync(post);
                    await _context.SaveChangesAsync();

                    if (model.SelectedCategoryIds != null)
                    {
                        // Add selected categories to the post
                        await UpdatePostCategoriesAsync(post.Id, model.SelectedCategoryIds);
                    }
                    else
                    {
                        var uncategorized = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "دسته بندی نشده");
                        if (uncategorized != null)
                        {
                            List<int> u = new List<int> { uncategorized.CategoryId };
                            await UpdatePostCategoriesAsync(post.Id, u);
                        }
                        
                    }
                    
                    
                    ModelState.Remove("Excerpt");
                    
                    
                    model = await CreatePostViewModelAsync(post);
                    
                    return View(model);
                }
                model = await CreateRawModelFromModel(model);
                return View(model);
            }
        }

        private async Task<PostCreateViewModel> CreateRawModelFromModel(PostCreateViewModel? _model=null)
        {
            var model = _model ?? new PostCreateViewModel();
            var currentUser = await _userManager.GetUserAsync(User);
            model.Author = currentUser.UserName;
            var categories = await _context.Categories.ToListAsync();
            model.AvailableCategories = categories.Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name }).ToList();
            model.Images = await _context.Images.ToListAsync();
            model.FeatureImage = await _context.Images.FindAsync(model.FeatureImageId);
            
            return model;
        }

        private async Task<PostCreateViewModel> CreatePostViewModelAsync(Post post)
        {
            var model = new PostCreateViewModel();
            var categories = await _context.Categories.ToListAsync();

            model.Title = post.Title;
            model.Content = post.Content;
            var postCategory = await _context.postCategories.FirstOrDefaultAsync(c => c.PostId == post.Id);

            
            

            model.AvailableCategories = categories.Select(c => 
                new SelectListItem 
                    { 
                        Value = c.CategoryId.ToString(),
                        Text = c.Name,
                        Selected = postCategory != null && c.CategoryId == postCategory.CategoryId
                    })
                .ToList();

            model.Images = await _context.Images.ToListAsync();
            model.CreatedDate = post.CreatedDate;
            model.UpdateDate = post.UpdatedDate;
            model.Excerpt = post.Excerpt;
            model.PostId = post.Id;
            model.FeatureImage = post?.FeaturedImage;
            model.FeatureImageId =post?.FeaturedImageId;
            
            //model.SelectedCategoryIds.Add(postCategory.CategoryId);
            var user = await _userManager.FindByIdAsync(post.AuthorId);
            if (user != null)
            {
                model.Author = user.UserName;
            }
            return model;
        }
        private Post MakeUpdateExistPostFromModel(PostCreateViewModel model,Post ExistPost)
        {
            
            var post = ExistPost;

            if(model.Title != ExistPost.Title)
                post.Title = model.Title;
            if(model.Content !=  ExistPost.Content)
                post.Content = model.Content;
            if(model.FeatureImageId != ExistPost.FeaturedImageId)
                post.FeaturedImageId = model.FeatureImageId;

            post.UpdatedDate = DateTime.Now;

            if (model.Excerpt != ExistPost.Excerpt)
                if (model.Excerpt == null)
                {
                    if(model.Content != null)
                    post.Excerpt = GetLimitedWordsFromHtml(model.Content, 15);
                }
                else
                {
                    post.Excerpt = GetLimitedWordsFromHtml(model.Excerpt, 15);
                }
                    

            return post;
        }
        private Post MakeNewPostFromModel(PostCreateViewModel model,string AuthorId)
        {

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                FeaturedImageId = model.FeatureImageId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                AuthorId = AuthorId,
                FeaturedImage = model.FeatureImage,
            };
            if (model.Excerpt == null)
            {
                if (model.Content != null)
                {
                    post.Excerpt = GetLimitedWordsFromHtml(model.Content, 15);
                }
            }
            else
            {
                post.Excerpt = GetLimitedWordsFromHtml(model.Excerpt, 15);
            }

            return post;
        }
        private async Task UpdatePostCategoriesAsync(int postId, List<int> newCategories)
        {
            // Remove existing categories asynchronously
            var existingCategories = await _context.postCategories
                .Where(pc => pc.PostId == postId)
                .ToListAsync();
            if(existingCategories != null && existingCategories.Count > 0)
                _context.postCategories.RemoveRange(existingCategories);

            // Add new categories asynchronously
            foreach (var category in newCategories)
            {
                _context.postCategories.Add(new PostCategory {PostId = postId , CategoryId = category});
            }

            await _context.SaveChangesAsync();
        }
        /// ////////////////////////-----Save Image-----/////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, string imageTitle, string imageDescription)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Handle the uploaded image, save it, and update the database with image details
                // For simplicity, you can save the image to a specific directory and store the file path in the database

                // Example: Save the image to the wwwroot/images directory
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Create an ImageModel instance and save it to the database
                var newImage = new Image
                {
                    ImageUrl = $"/images/{fileName}", // Relative URL to the saved image
                    FileName = imageTitle,
                    Caption = imageDescription,
                };

                // Save the new image to the database using your DbContext
                _context.Images.Add(newImage);
                await _context.SaveChangesAsync();

                // Return a JSON response with the image details
                return Json(new
                {
                    success = true,
                    newImage.ImageUrl,
                    newImage.FileName,
                    newImage.Caption,
                    newImage.Id
                });
            }

            // If no image is uploaded or if there was an error, return an error JSON response
            return Json(new { success = false, errorMessage = "Image upload failed." });

        }
        private string[] SaveFeaturedImage(ImageViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Implement the logic to save the image to your desired location.
                // For example, you can save it to the wwwroot/images folder:
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid() + "_" + model.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
                var img = new Image 
                {
                    FileName = model.ImageFile.FileName,
                    Caption = model.Caption,
                    AltText = model.AltText,
                    ImageUrl = "/images/" + uniqueFileName,
                    UploadDate = DateTime.Now
                };
                _context.Images.Add(img);
                _context.SaveChangesAsync();
                int id = img.Id;
                string address = "/images/" + uniqueFileName;
                string[] result = [id.ToString(), address];
                return result;
            }
            else
            {
                // Handle the case where no image was provided.
                return null; // You can return a default image path or handle it as needed.
            }
        }

        /////////////////////////////-------Tag--------/////////////////////////////////////////////
        public async Task<IActionResult> AddNewTag(PostCreateViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.NewTag))
            {
                // Add the new tag to your database (you need to implement this logic)
                // You can use Entity Framework or any other data access method.
                // After adding to the database, you can refresh the available tags list.
                var tag = new Tag
                {
                    Name = viewModel.NewTag,
                };
                _context.Tags.Add(tag);
                _context.SaveChanges();
                
                // dbContext.Tags.Add(new Tag { Name = viewModel.NewTag });
                // dbContext.SaveChanges();

                // Refresh available tags (retrieve them from the database)
                var tags = await _context.Tags.ToListAsync();
                viewModel.AvailableTags = tags.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList(); // Implement GetAvailableTags() to fetch tags from the database
                //viewModel.SelectedTagIds
            }

            // Return the updated ViewModel to the view
            return View("CreatePost", viewModel);
        }
        private string GetExcerpt(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return "";
            }
            string PlainText = RemoveHtmlTags(Text);
            if (string.IsNullOrEmpty(PlainText))
            {
                return "";
            }
            int limit = 15; // Change this to the desired number of words you want to extract.

            string[] words = PlainText.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (limit <= words.Length)
            {
                string[] limitedWords = new string[limit];
                Array.Copy(words, limitedWords, limit);

                string limitedText = string.Join(" ", limitedWords);
                return limitedText;
            }
            else
            {
                return PlainText;
            }
        }
        
        private string GetLimitedWordsFromHtml(string html, int wordLimit)
        {
            if (string.IsNullOrEmpty(html) || wordLimit <= 0)
                return string.Empty;

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // Remove HTML tags and entities and extract plain text
            var textNodes = htmlDocument.DocumentNode.DescendantsAndSelf()
                .Where(n => n.NodeType == HtmlNodeType.Text)
                .Select(n => n.InnerHtml.Replace("&nbsp;", " ").Trim());

            // Concatenate the plain text and split it into words
            var plainText = string.Join(" ", textNodes);
            var words = plainText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Take the first 'wordLimit' words
            var limitedWords = string.Join(" ", words.Take(wordLimit));

            return limitedWords;
        }
        private string RemoveHtmlTags(string text)
        {
            // Use a regular expression to remove HTML tags
            return Regex.Replace(text, "<.*?>", "");
        }

    }
}
