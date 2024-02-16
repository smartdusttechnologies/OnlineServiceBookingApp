using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServcieBooking.Business.Interface;
using ServcieBooking.Business.PipelineBehaviors;
using ServcieBooking.Business.Features.Resturant;
using ServiceBooking.Business.Data.Repository.Interfaces;
using ServiceBooking.Business.Repository;
using ServiceBooking.Business.Data.Repository.Interfaces.Security;
using ServiceBooking.Buisness.Repository;
using ServiceBooking.Buisness.Repository.Interfaces;
using ServiceBooking.Buisness.Features.Authentication.Commands;
using ServiceBooking.Buisness.Features.SecurityParamters.Queries;
using ServiceBooking.Buisness.Features.User.Commands;
using ServiceBooking.Buisness.Features.User.Queries;
using ServiceBooking.Buisness.Features.Order.Commands;
using ServcieBooking.Business.Features.Orders.Queries;
using ServiceBooking.Buisness.Features.Coupon.Commands;

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
            #region Authorization Handler Register Start
            services.AddTransient<IAuthorizationRule<GetResturant.Command>, GetResturant.Authorization>();
            services.AddTransient<IAuthorizationRule<GetByIdResturant.Command>, GetByIdResturant.Authorization>();
            services.AddTransient<IAuthorizationRule<GetConfiguration.Command>, GetConfiguration.Authorization>();

            services.AddTransient<IAuthorizationRule<ChangePassword.Command>, ChangePassword.Authorization>();
            services.AddTransient<IAuthorizationRule<SignUp.Command>, SignUp.Authorization>();
            services.AddTransient<IAuthorizationRule<EmailExist.Command>, EmailExist.Authorization>();
            services.AddTransient<IAuthorizationRule<Login.Command>, Login.Authorization>();
            services.AddTransient<IAuthorizationRule<GetOrganizations.Command>, GetOrganizations.Authorization>();
            services.AddTransient<IAuthorizationRule<ChangePasswordPolicy.Command>, ChangePasswordPolicy.Authorization>();
            services.AddTransient<IAuthorizationRule<GetSecurityParameter.Command>, GetSecurityParameter.Authorization>();
            services.AddTransient<IAuthorizationRule<ValidateEmail.Command>, ValidateEmail.Authorization>();
            services.AddTransient<IAuthorizationRule<ValidatePasswordPolicy.Command>, ValidatePasswordPolicy.Authorization>();
            services.AddTransient<IAuthorizationRule<ValidatePhoneNumber.Command>, ValidatePhoneNumber.Authorization>();
            services.AddTransient<IAuthorizationRule<NewOrder.Command>, NewOrder.Authorization>();
            services.AddTransient<IAuthorizationRule<InsertUser.Command>, InsertUser.Authorization>();
            services.AddTransient<IAuthorizationRule<GetUser.Command>, GetUser.Authorization>();
            services.AddTransient<IAuthorizationRule<Profile.Command>, Profile.Authorization>();
            services.AddTransient<IAuthorizationRule<GetOrders.Command>, GetOrders.Authorization>();
            services.AddTransient<IAuthorizationRule<ApplyCoupon.Command>, ApplyCoupon.Authorization>();
            #endregion
            #region Logger Handler Register Start
            services.AddTransient<ILoggerRule<GetResturant.Command>, GetResturant.Logger>();
            services.AddTransient<ILoggerRule<GetByIdResturant.Command>, GetByIdResturant.Logger>();
            services.AddTransient<ILoggerRule<GetConfiguration.Command>, GetConfiguration.Logger>();
            services.AddTransient<ILoggerRule<ChangePassword.Command>, ChangePassword.Logger>();
            services.AddTransient<ILoggerRule<SignUp.Command>, SignUp.Logger>();
            services.AddTransient<ILoggerRule<EmailExist.Command>, EmailExist.Logger>();
            services.AddTransient<ILoggerRule<Login.Command>, Login.Logger>();
            services.AddTransient<ILoggerRule<GetOrganizations.Command>, GetOrganizations.Logger>();
            services.AddTransient<ILoggerRule<ChangePasswordPolicy.Command>, ChangePasswordPolicy.Logger>();
            services.AddTransient<ILoggerRule<GetSecurityParameter.Command>, GetSecurityParameter.Logger>();
            services.AddTransient<ILoggerRule<ValidateEmail.Command>, ValidateEmail.Logger>();
            services.AddTransient<ILoggerRule<ValidatePasswordPolicy.Command>, ValidatePasswordPolicy.Logger>();
            services.AddTransient<ILoggerRule<ValidatePhoneNumber.Command>, ValidatePhoneNumber.Logger>();
            services.AddTransient<ILoggerRule<NewOrder.Command>, NewOrder.Logger>();
            services.AddTransient<ILoggerRule<Profile.Command>, Profile.Logger>();
            services.AddTransient<ILoggerRule<GetUser.Command>, GetUser.Logger>();
            services.AddTransient<ILoggerRule<InsertUser.Command>, InsertUser.Logger>();
            services.AddTransient<ILoggerRule<NewOrder.Command>, NewOrder.Logger>();
            services.AddTransient<ILoggerRule<GetOrders.Command>, GetOrders.Logger>();
            services.AddTransient<ILoggerRule<ApplyCoupon.Command>, ApplyCoupon.Logger>();
            #endregion
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IContactRepository,ContactRepository>();
            services.AddTransient<IAuthenticationRepository,AuthenticationRepository>();
            services.AddTransient<IRoleRepository,RoleRepository>();
            services.AddTransient<ISecurityParameterRepository,SecurityParameterRepository>();
            services.AddTransient<ISystemRepository,SystemRepository>();
            services.AddTransient<ILoggerRepository,LoggerRepository>();
            services.AddTransient<IOrganizationRepository,OrganizationRepository>();
            return services;

        }
    }
}
