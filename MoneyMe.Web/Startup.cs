using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Data.Interface;
using MoneyMe.Infrastructure.Data.Query;
using MoneyMe.Web.Service.Command.Implementation;
using MoneyMe.Web.Service.Command.Interface;
using MoneyMe.Web.Service.Query.Implementation;
using MoneyMe.Web.Service.Query.Interface;

namespace MoneyMe.Web
{
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
            services.AddAutoMapper(typeof(Program).Assembly);
            var connectionString = Configuration.GetSection("Database").GetValue<string>("ConnectionString");
            services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
            ServiceDependency(services);
            InfrastructureDependency(services);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "MoenyMe",
                  areaName: "MoenyMe",
                   pattern: "MoenyMe/{controller=Transaction}/{action=GetCustomer}/{id?}");
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ServiceDependency(IServiceCollection services)
        {
            services.AddTransient<IGetCustomerQuery, GetCustomerQuery>();
            services.AddTransient<IGetRepaymentAmountQuery, GetRepaymentAmountQuery>();
            services.AddTransient<IAddLoanCommand, AddLoanCommand>();
        }

        private void InfrastructureDependency(IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IGenericRepository, GenericRepository>();
        }
    }
}
