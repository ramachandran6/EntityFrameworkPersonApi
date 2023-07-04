using Microsoft.EntityFrameworkCore;
using WebApiSample.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<PersonDBContext>(options => options.UseInMemoryDatabase("ElevatorSystem")); // this is for InMemory that will take some space in RAM and acts as database
builder.Services.AddDbContext<PersonDBContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("nafteam4"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
