using Courstick.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Courstick.Core.Models;
using Courstick.Core.Services;
using Courstick.Infrastructure;
using Courstick.Infrastructure.Database.Repositories;
using SignalRApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddWebOptimizer(x =>
{
    x.MinifyJsFiles("js/**/*.js");
    x.MinifyCssFiles("css/**/*.css");
});

builder.Services.AddSession();

var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();

builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<CommentService>();


builder.Services.AddIdentity<User, Role>(o =>
{
    if(builder.Environment.IsDevelopment())
    {
        o.Password.RequireDigit = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
    }
}).AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
});

using var scope = app.Services.CreateScope();
var scopedProvider = scope.ServiceProvider;

try
{
    var roleManager = scopedProvider.GetRequiredService<RoleManager<Role>>();
    var seeding = new Seeding(roleManager);
    await seeding.Seed();
}
catch (Exception){}

app.Run();
