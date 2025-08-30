using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviors;
using MediatR;

namespace Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Provides extension methods for configuring services in the Application layer.
        /// This class centralizes dependency registration for services like MediatR, AutoMapper, and FluentValidation.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            // --- Register MediatR Pipeline Behaviors ---
            // Behaviors act as a middleware for the MediatR pipeline.
            // They are executed in the order they are registered.
            // A request's flow will be:
            // Request -> LoggingBehavior -> PerformanceBehavior -> ValidationBehavior -> RequestHandler

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
