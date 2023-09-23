using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RajaFath.Models;


namespace RajaFath.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
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
        ///

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
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var roles = await _userManager.GetRolesAsync(user);
        //    var userWithRoles = new UserListViewModel
        //    {
        //        UserId = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Role = roles.FirstOrDefault() // Assuming a user can have only one role
        //    };



        //    var roleNames = _roleManager.Roles.Select(role => role.Name).ToList();

        //    var model = new UserListViewModel
        //    {
        //        UserId = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        // Assuming you have a property in your EditUserViewModel for RoleList
        //        RoleList = new SelectList(roleNames, userWithRoles.Role) // userRole is the user's current role
        //    };
        //    return View(model);
        //}

        [HttpGet]
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

            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
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
    }
}
