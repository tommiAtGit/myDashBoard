using myNotesService.Services;
using myNotesService.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services
builder.Services.AddScoped<IHealthService, HealthService>();

// Register AutoMapper with DI
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>()); 
builder.WebHost.UseUrls("http://0.0.0.0:80");

// Register custom services
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IGeneralNotesService, GeneralNotesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3000/") // Allow both variations
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseRouting(); // Explicitly add routing

app.UseCors("AllowSpecificOrigin"); // Apply CORS policy between routing and controllers

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();