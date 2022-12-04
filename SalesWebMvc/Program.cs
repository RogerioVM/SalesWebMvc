using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddScoped<SeedingService>(); // injetando depend�ncia
builder.Services.AddScoped<SellerService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();


#region Seeding Service

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed(); // Forma no .NET 6 para popular o DB como seeding service
#endregion


#region Outra forma de resolver o problema do Seeding Service
// Na classe HomeController, utilizar o c�digo abaixo:

//public HomeController(ILogger<HomeController> logger, SeedingService seedingService)

//{

//    _logger = logger;

//    seedingService.Seed();

//}

#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
