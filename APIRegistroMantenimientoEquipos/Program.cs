using APIRegistroMantenimientoEquipos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la colección

// Agregar la configuración de CORS antes de la creación de la app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://sprightly-babka-9a6882.netlify.app") // Asegúrate de usar la URL correcta
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
});

// Crear la aplicación
var app = builder.Build();

// Iniciar la base de datos con valores predeterminados si es necesario
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer dbInitializer = new DbInitializer(context);
    dbInitializer.Seed();
}

// Activar Swagger para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");  
    });
}

// En producción también puedes activarlo, pero asegúrate de que está accesible
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");  
});



// Usar CORS antes de cualquier middleware de autorización o enrutamiento
app.UseCors("AllowFrontend"); // Activar CORS

app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
