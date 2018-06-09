using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure;
using PMS.Web.UI.Core.Interfaces;
using PMS.Web.UI.Core.Models;
using PMS.Web.UI.ViewModels;
using PureSmiles.Services;

namespace PMS.Web.UI
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_hostingEnvironment.EnvironmentName.ToLower()}.json", optional: true);
            builder.AddEnvironmentVariables();
            _configurationRoot = builder.Build();

            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddTransient<IGoalRepository, GoalRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IEmailSender, EmailSender>();

            AutoMapper.Mapper.Initialize(_configurationRoot =>
            {
                _configurationRoot.CreateMap<GoalViewModel, Goal>().ReverseMap();
                _configurationRoot.CreateMap<Goal, GoalViewModel>().ReverseMap();

                _configurationRoot.CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                _configurationRoot.CreateMap<Employee, EmployeeViewModel>().ReverseMap();

            });

            services.AddMvc();
            services.AddMvc(options => options.MaxModelValidationErrors = 50);

            // Read email settings
            services.Configure<EmailConfig>(_configuration.GetSection("Email"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // http://weblog.afsharm.com/2017/09/ef-core-automatic-migration/
            //http://jaliyaudagedara.blogspot.in/2017/10/ef-core-automatic-migrations.html
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                AppDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                //dbContext.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseAuthentication();//setup cookies

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=account}/{action=login}/{id?}");
            });

            app.UseCors("AllowAll");
        }
    }
}
