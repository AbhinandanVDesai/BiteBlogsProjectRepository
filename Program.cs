using BiteBlogs.Models;
using BiteBlogs.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BiteBlogDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BiteCons")));

builder.Services.AddDbContext<AuthDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("BiteBlogsAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


builder.Services.AddScoped<ITagRepository, TagRepositoryClass>();
builder.Services.AddScoped<IBlogRepository, BlogRepositoryClass>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikesRepository>();
builder.Services.AddScoped<ICommentRepository , CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
                                                    
var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
