using Application.Mappings;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Queries.GetFilteredUsers;
using Application.UseCases.Users.Queries.GetUserById;
using Application.UseCases.Users.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection.Metadata;

namespace Api
{
    public class Program
    {
        //[Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            ConfigureLogs();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DotNet8ApiStarter API",
                    Version = "v1",
                    Description = "API de ejemplo con arquitectura por capas y FluentValidation"
                });
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommandHandler>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserByIdQueryHandler>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetFilteredUsersQueryHandler>());

            var configuration = builder.Configuration;

            builder.Services.AddInfrastructureServices(configuration);
            builder.Host.UseSerilog();

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
        }

        private static void ConfigureLogs()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console() // Opcional
                .WriteTo.File(
                    formatter: new RenderedCompactJsonFormatter(),
                    path: "Logs/log-.txt",                        
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    fileSizeLimitBytes: 10_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                )
                .CreateLogger();
        }
    }
}
