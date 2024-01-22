using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RajaFath;
using RajaFath.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
},ServiceLifetime.Scoped);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric=false;
            options.SignIn.RequireConfirmedAccount = false;
            
        })
          .AddRoles<IdentityRole>()
          .AddErrorDescriber<CustomIdentityErrorDescriber>()
          .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Add services for file uploads
builder.Services.AddHttpContextAccessor();

// Configure the upload folder
var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images"); // Change the folder path as needed
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "PostList",
        pattern: "Admin/PostList",
        defaults: new { controller = "Admin", action = "PostList" }
        );

    endpoints.MapControllerRoute(
        name: "Category",
        pattern: "{category}",
        defaults: new { controller = "Category", action = "Category" },
        constraints: new { category = new SlugifyParameterTransformer() }
        );

    endpoints.MapControllerRoute(
        name: "Page",
        pattern: "{slug}",
        defaults: new { controller = "Page", action = "show" },
        constraints: new { slug = new SlugifyParameterTransformer() }
    );
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});


app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");
app.MapRazorPages();

app.Run();
