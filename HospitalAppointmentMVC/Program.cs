// Create the application builder.
var builder = WebApplication.CreateBuilder(args);

// Add MVC services.
builder.Services.AddControllersWithViews();

// Add session support.
builder.Services.AddSession();

// Add MemoryCache for storing doctor details for a short time.
builder.Services.AddMemoryCache();

// Add response caching support.
builder.Services.AddResponseCaching();

var app = builder.Build();

// Use the normal ASP.NET Core middleware.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable response caching.
app.UseResponseCaching();

// Enable session before the controller is called.
app.UseSession();

// Set the default route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hospital}/{action=Index}/{id?}");

app.Run();
