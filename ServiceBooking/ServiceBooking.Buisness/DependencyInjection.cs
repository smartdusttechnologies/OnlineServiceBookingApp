using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServcieBooking.Business.Interface;
using ServcieBooking.Business.PipelineBehaviors;
using ServcieBooking.Business.Features.Resturant;

namespace ServcieBooking.Business
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IAuthorizationRule<GetResturant.Command>, GetResturant.Authorization>();
            services.AddTransient<IAuthorizationRule<GetByIdResturant.Command>, GetByIdResturant.Authorization>();
            //services.AddTransient<IAuthorizationRule<CreateUiPageType.Command>, CreateUiPageType.Authorization>();
            //services.AddTransient<IAuthorizationRule<UpdateUiPageType.Command>, UpdateUiPageType.Authorization>();
            //services.AddTransient<IAuthorizationRule<DeleteUiPageType.Command>, DeleteUiPageType.Authorization>();
            return services;

        }
    }
}
