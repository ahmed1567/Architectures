using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Ports;
using TaskManager.Application.UseCases;
using TaskManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Entity Framework with in-memory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagerDb"));

// Register dependencies
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<CreateJobUseCase>();
builder.Services.AddScoped<GetAllJobsUseCase>();
builder.Services.AddScoped<CompleteJobUseCase>();

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

