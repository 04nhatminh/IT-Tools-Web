using IT_Tools_Web.Business.Services;
using IT_Tools_Web.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ToolService>();
builder.Services.AddScoped<ToolLoaderService>();

// Add session service
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session tồn tại
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseSession();

// Quét thư mục Tools và load dữ liệu vào database
using (var scope = app.Services.CreateScope())
{
    var toolLoader = scope.ServiceProvider.GetRequiredService<ToolLoaderService>();
    var toolsDirectory = Path.Combine(app.Environment.ContentRootPath, "Tools");
    toolLoader.LoadToolsFromDirectory(toolsDirectory);
}

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
