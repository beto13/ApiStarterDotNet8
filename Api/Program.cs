using Application.Mappings;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Queries.GetUserById;
using Application.UseCases.Users.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;

namespace Api
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
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
            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetFilteredUsersQueryHandler>());

            var configuration = builder.Configuration;
            builder.Services.AddInfrastructureServices(configuration);
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
    }
}
