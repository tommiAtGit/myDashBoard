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
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.WebHost.UseUrls("http://0.0.0.0:80");

// Register custom services
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IGeneralNotesService, GeneralNotesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Allow requests from the React app
              .AllowAnyHeader() // Allow all headers
              .AllowAnyMethod(); // Allow GET, POST, PUT, DELETE, etc.
    });
});

var app = builder.Build();

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