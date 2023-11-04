using Microsoft.EntityFrameworkCore;
using WebApplication2.Services;
using WebApplication2.DB;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<JobQueue<KitchenJob>>();
builder.Services.AddSingleton<JobQueue<DelivererJob>>();
builder.Services.AddHostedService<KitchenService>();
builder.Services.AddHostedService<DeliverersService>();

//add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source=DB/database.db"),
    contextLifetime: ServiceLifetime.Scoped, 
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();