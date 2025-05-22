using Microsoft.EntityFrameworkCore;
using BurakSteam.Models;
using BurakSteam.Data;
using Microsoft.AspNetCore.Identity;
using static BurakSteam.Data.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsýný ekliyoruz ve Lazy Loading'i etkinleþtiriyoruz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());  // Lazy Loading'i etkinleþtiriyoruz

// MVC controller ve view'lerini ekliyoruz
builder.Services.AddControllersWithViews();

// Identity servislerini ekliyoruz (Kullanýcý yönetimi için)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
   
    .AddDefaultTokenProviders();

var app = builder.Build();

// HTTP request pipeline yapýlandýrmasýný yapýyoruz.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Geliþtirme ortamý için hata sayfasý
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Üretim ortamý için hata yönetimi
    app.UseHsts();  // HTTPS zorunluluðu
}

app.UseHttpsRedirection();  // HTTPS'ye yönlendirme
app.UseStaticFiles();  // Statik dosyalarý kullanabilmek için

app.UseRouting();  // Yönlendirme iþlemleri

app.UseAuthentication();  // Kimlik doðrulama iþlemleri
app.UseAuthorization();  // Yetkilendirme iþlemleri

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Varsayýlan rota

app.Run();  // Uygulamayý baþlat
