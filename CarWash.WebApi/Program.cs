using CarWash.Application.IRepositoryInterfaces;
using CarWash.Application.IServiceInterfaces;
using CarWash.Application.Mapping;
using CarWash.Application.Services;
using CarWash.Infrastructure.Data;
using CarWash.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

// 5️⃣ HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 6️⃣ HttpClient + Typed client
builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5261/"); // UserService-ի HTTP port
});

builder.Services.AddScoped<IUserServiceClient, UserServiceClient>(); // ✅ interface-ով

// 7️⃣ JWT Authentication
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
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// 8️⃣ Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarWash", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{ Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

// 9️⃣ Controllers + JSON settings
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 🔟 CORS
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
