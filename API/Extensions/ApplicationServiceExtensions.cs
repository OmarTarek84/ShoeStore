using API.Helpers;
using Core.Interfaces;
using InfraStructure.Data;
using InfraStructure.Errors;
using InfraStructure.Helpers;
using InfraStructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DB_CONNECTION_STRING"), b => b.MigrationsAssembly("InfraStructure"));
            });
            services.Configure<JWT>(config.GetSection("JWT"));
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<LogUserActivity>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            // API Validation Errors
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse { Message = errors[0] };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
