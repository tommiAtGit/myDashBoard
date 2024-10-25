using AutoMapper;
using myFinanceService.Services;
using myFinanceService.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper with DI
builder.Services.AddAutoMapper(typeof(MappingProfile)); 

// Register custom services
builder.Services.AddScoped<IMyHealthService, MyHealthService>();
builder.Services.AddScoped<IFinanceTrackerService, FinanceTrackerService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();

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
