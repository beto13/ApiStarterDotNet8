using Application.Dtos.Posts;
using Application.Dtos.User;
using Application.Filtering.Factories;
using Application.Filtering.Interfaces;
using Application.Interfaces.Persistence;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
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
            services.AddScoped<IFilterService<Post, PostFilterDto>, FilterService<Post, PostFilterDto>>();
            services.AddScoped<IFilterStrategyFactory<User, UserFilterDto>, UserFilterStrategyFactory>();
            services.AddScoped<IFilterStrategyFactory<Post, PostFilterDto>, PostFilterStrategyFactory>();

            return services;
        }
    }
}
