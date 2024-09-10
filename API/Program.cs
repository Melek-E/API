using API.Data;
using API.Services;
using API.Models.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Default property names
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<TestService>();

// Configure the database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QuizzDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<QuizzDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = false;
});

// Configure session and cookie-based authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
    options.LoginPath = "/api/auth/login";
    options.AccessDeniedPath = "/api/auth/access-denied";
    options.SlidingExpiration = true;
});

builder.Services.AddDistributedMemoryCache(); // In-memory cache for session storage

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3); // Session timeout duration
    options.Cookie.HttpOnly = true; // can only be modified when it reaches the server
    options.Cookie.IsEssential = true; // Mark session cookie as essential
});
builder.Services.AddCors();
var app = builder.Build();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRolesAsync(roleManager);  // Seed roles like "Admin"

    // Seed super admin user
    await Seed.SeedSuperAdmin(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession(); // Enable session management
app.UseAuthentication(); // Enable cookie-based authentication
app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200", "https://localhost:7112", "http://localhost:7112"));
app.Run();

// Seed roles method
static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] {"SuperAdmin", "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}



