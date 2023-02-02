using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnScreenKeyboard.Application.Common.Behaviors;
using System.Reflection;
using OnScreenKeyboard.Application.Services.Keyboard;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddScoped<IKeyboard, StandardKeyboard>();
            return services;
        }
    }
}

