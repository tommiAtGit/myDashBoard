
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





// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Allow your React app
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowLocalhost"); // Use the defined CORS policy

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

