using GuardTour_API.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConnectDB.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    
    c.SwaggerEndpoint("/GuardTourAPI/swagger/v1/swagger.json", "GaurdTour API V1.0");

    c.RoutePrefix = "swagger";
});


app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
