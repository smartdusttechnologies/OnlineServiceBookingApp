using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServcieBooking.Business.Interface;
using ServcieBooking.Business.PipelineBehaviors;
using ServcieBooking.Business.Features.Resturant;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Repository;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;

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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IContactRepository,ContactRepository>();
            services.AddTransient<IAuthenticationRepository,AuthenticationRepository>();
            services.AddTransient<IRoleRepository,RoleRepository>();
            services.AddTransient<ISecurityParameterRepository,SecurityParameterRepository>();
            return services;

        }
    }
}
