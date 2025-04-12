using Application.Interfaces.Persistence;
using Application.Interfaces.Services;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Filters.Factories;
using Domain.Filters.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFilterService<User, UserFilterDto>, FilterService<User, UserFilterDto>>();
            services.AddScoped<IFilterStrategyFactory<User, UserFilterDto>, UserFilterStrategyFactory>();

            return services;
        }
    }
}
