using APIRegistroMantenimientoEquipos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la colecci�n

// Agregar la configuraci�n de CORS antes de la creaci�n de la app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://sprightly-babka-9a6882.netlify.app") // Aseg�rate de usar la URL correcta
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

// Crear la aplicaci�n
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

// En producci�n tambi�n puedes activarlo, pero aseg�rate de que est� accesible
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");  
});



// Usar CORS antes de cualquier middleware de autorizaci�n o enrutamiento
app.UseCors("AllowFrontend"); // Activar CORS

app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
