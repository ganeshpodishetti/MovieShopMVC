using MovieShop.Infra.Data;
using OptionsExtension = MovieShop.MVC.Extensions.OptionsExtension;
using ServiceExtension = MovieShop.MVC.Extensions.ServiceExtension;
using AuthenticationExtension = MovieShop.MVC.Extensions.AuthenticationExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

OptionsExtension.AddOptions(builder);
builder.Services.AddDatabase();
AuthenticationExtension.AddAuthentication(builder.Services);
ServiceExtension.AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ensure static files middleware is explicitly added
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
       "default",
       "{controller=Home}/{action=Index}/{id?}")
   .WithStaticAssets();


app.Run();