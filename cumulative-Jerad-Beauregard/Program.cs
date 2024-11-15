using System;
using cumulative_Jerad_Beauregard.Controllers;
using cumulative_Jerad_Beauregard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database THIS HAS TO BE ADDED FOR THE DEPENDENCY INJECTOR TO WORK
builder.Services.AddScoped<SchoolDbContext>();
builder.Services.AddScoped<TeacherAPIController>();
builder.Services.AddScoped<StudentAPIController>();
builder.Services.AddScoped<CourseAPIController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
