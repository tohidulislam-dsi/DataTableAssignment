using DataTableAssignment.API.Data;
using DataTableAssignment.API.Mapping;
using DataTableAssignment.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataTableAssignmentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DataTableASsignmentConnectionString")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 
builder.Services.AddAutoMapper(typeof(AutomapperProfiles));    
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
