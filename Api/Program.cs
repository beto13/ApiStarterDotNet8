using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddValidatorsFromAssemblyContaining<>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddControllers();

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

            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<>());

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
