using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServcieBooking.Buisness.Features.UiPageType;
using ServcieBooking.Buisness.Features.UiPageType.Commands;
using ServcieBooking.Buisness.Interface;
using ServcieBooking.Buisness.Models;
using ServcieBooking.Buisness.PipelineBehaviors;

namespace ServcieBooking.Buisness
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IAuthorizationRule<GetUiPageType.Command>, GetUiPageType.Authorization>();
            services.AddTransient<IAuthorizationRule<GetByIdUiPageType.Command>, GetByIdUiPageType.Authorization>();
            services.AddTransient<IAuthorizationRule<CreateUiPageType.Command>, CreateUiPageType.Authorization>();
            services.AddTransient<IAuthorizationRule<UpdateUiPageType.Command>, UpdateUiPageType.Authorization>();
            services.AddTransient<IAuthorizationRule<DeleteUiPageType.Command>, DeleteUiPageType.Authorization>();
            return services;

        }
    }
}
