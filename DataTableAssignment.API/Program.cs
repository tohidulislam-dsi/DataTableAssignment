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
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
