using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Application.Mapping;
using CarWash.Application.Services;
using CarWash.Infrastructure.Data;
using CarWash.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 1️⃣ Database Configuration
// =====================================================
builder.Services.AddDbContext<CarWashContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================
// 2️⃣ AutoMapper Configuration
// =====================================================
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// =====================================================
// 3️⃣ Repository Layer DI
// =====================================================
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWashStationRepository, WashStationRepository>();

// =====================================================
// 4️⃣ Service Layer DI
// =====================================================
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWashStationService, WashStationService>();

// =====================================================
// 5️⃣ Swagger Configuration
// =====================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================================
// 6️⃣ Controller Configuration
// =====================================================
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// =====================================================
// 7️⃣ CORS (optional, but recommended for frontend integration)
// =====================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// =====================================================
// 8️⃣ Build App
// =====================================================
var app = builder.Build();

// =====================================================
// 9️⃣ Middleware Pipeline
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");  // 👈 useful if frontend (React, Angular...) connects later
app.UseAuthorization();

app.MapControllers();

app.Run();
