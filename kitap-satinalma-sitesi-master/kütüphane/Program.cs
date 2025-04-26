var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session services
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) kullanımı
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyalar için middleware

app.UseRouting(); // Yönlendirme middleware

app.UseAuthorization(); // Yetkilendirme middleware

// Use session middleware
app.UseSession();

app.MapRazorPages(); // Razor Pages için yönlendirme

app.Run(); // Uygulamayı çalıştır
