using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<QuizzDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<QuestionService>();
        services.AddScoped<TestService>();
        services.AddScoped<FrameworkService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddSignalR();

        return services;
    }
}