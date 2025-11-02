using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Application.Mapping;
using CarWash.Application.Services;
using CarWash.Infrastructure.Caching;
using CarWash.Infrastructure.Data;
using CarWash.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Database
builder.Services.AddDbContext<CarWashContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 3️⃣ Repositories
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWashStationRepository, WashStationRepository>();

// 4️⃣ Services
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWashStationService, WashStationService>();

// 5️⃣ Redis cache registration
var configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
if (configuration != null)
{
    var redis = ConnectionMultiplexer.Connect(
        configuration
    );
    builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
}

builder.Services.AddScoped<ICacheService, RedisCacheService>();

// 6️⃣ HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 7️⃣ HttpClient + Typed client
builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5261/");
});
builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();

// 8️⃣ JWT Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["Key"] ?? throw new Exception("Jwt:Key missing in appsettings.json");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// 9️⃣ Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CarWash API",
        Version = "v1",
        Description = "CarWash system API with JWT authentication"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: **Bearer {your token}**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🔟 Controllers + JSON settings
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 1️⃣1️⃣ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
