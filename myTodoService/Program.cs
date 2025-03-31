
using myTodoService.Services;
using myTodoService.Mapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper with DI
builder.Services.AddAutoMapper(typeof(MappingProfile)); 

builder.WebHost.UseUrls("http://0.0.0.0:80");

// Register custom services
builder.Services.AddScoped<IMyHealthService, MyHealthService>();
builder.Services.AddScoped<ITodoService, TodoService>();



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

app.UseCors("AllowSpecificOrigin"); // Use the defined CORS policy

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

