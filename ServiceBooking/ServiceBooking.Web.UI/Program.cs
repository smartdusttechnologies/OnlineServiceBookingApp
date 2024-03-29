using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using ServcieBooking.Business;
using ServcieBooking.Business.Infrastructure;
using ServiceBooking.Buisness.Core.Models.Context;
using ServiceBooking.Buisness.Repository;
using ServiceBooking.Buisness.Repository.Interfaces;

namespace ServcieBooking.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();
            //services.AddDbContext<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("Host=localhost;Port=5432;Database=raj;Username=postgres;Password=root;"),
        b => b.MigrationsAssembly("ServiceBooking.Buisness")));
            // dotnet ef dbcontext scaffold  Microsoft.EntityFrameworkCore.SqlServer - o Models
            //dotnet ef dbcontext scaffold "Server=LAPTOP-RLHH2AOR\\SQLEXPRESS;Database=Maui; User ID=sa;Password=admin@123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c YourDbContextName

            // services.AddDbContext<AppDbContext>(options =>
            //options.UseSqlServer("Server=LAPTOP-RLHH2AOR\\SQLEXPRESS;Database=Maui; User ID=sa;Password=admin@123;TrustServerCertificate=True"));
            services.AddScoped<IResturantRepository,ResturantRepository>();
            services.AddScoped<IConnectionFactory,ConnectionFactory>();
            services.AddScoped<IResturantRepository,ResturantRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddApplication();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            //Repository DI
            //Authorization Handler Initalization End
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseCors();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}