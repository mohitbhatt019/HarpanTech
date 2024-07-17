
using ReportsServiceApi.DbContext;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var configServices = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("BaseDBConnection");
builder.Services.AddDbContext<BaseDBContext>(op =>
{
    op.UseSqlServer(connectionString);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
