var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

var app = builder.Build();

// Manejo global de excepciones
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // Error interno del servidor
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(
            new
            {
                StatusCode = 500,
                Message = "Ocurrió un error inesperado. Por favor, inténtalo más tarde.",
            }.ToString()
        );
    });
});

app.UseAuthorization();
app.MapControllers();
app.Run();
