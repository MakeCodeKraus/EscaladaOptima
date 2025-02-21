using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EscaladaOptima.Data;
using EscaladaOptima.Services;

var builder = WebApplication.CreateBuilder(args);

// Habilitar controladores y vistas
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Escalada Optima API", Version = "v1" });
});

// Agregar servicios
builder.Services.AddScoped<EscaladaService>();

// Configurar base de datos (SQLite en este caso)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilitar CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escalada Optima API v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Escalada}/{action=Index}/{id?}");

app.MapControllers(); // Mapea la API


app.Run();
