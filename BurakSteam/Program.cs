using Microsoft.EntityFrameworkCore;
using BurakSteam.Models;
using BurakSteam.Data;
using Microsoft.AspNetCore.Identity;
using static BurakSteam.Data.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� ekliyoruz ve Lazy Loading'i etkinle�tiriyoruz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());  // Lazy Loading'i etkinle�tiriyoruz

// MVC controller ve view'lerini ekliyoruz
builder.Services.AddControllersWithViews();

// Identity servislerini ekliyoruz (Kullan�c� y�netimi i�in)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
   
    .AddDefaultTokenProviders();

var app = builder.Build();

// HTTP request pipeline yap�land�rmas�n� yap�yoruz.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Geli�tirme ortam� i�in hata sayfas�
}
else
{
    app.UseExceptionHandler("/Home/Error");  // �retim ortam� i�in hata y�netimi
    app.UseHsts();  // HTTPS zorunlulu�u
}

app.UseHttpsRedirection();  // HTTPS'ye y�nlendirme
app.UseStaticFiles();  // Statik dosyalar� kullanabilmek i�in

app.UseRouting();  // Y�nlendirme i�lemleri

app.UseAuthentication();  // Kimlik do�rulama i�lemleri
app.UseAuthorization();  // Yetkilendirme i�lemleri

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Varsay�lan rota

app.Run();  // Uygulamay� ba�lat
