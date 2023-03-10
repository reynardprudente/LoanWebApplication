using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyMe.Application.Command.Implementation;
using MoneyMe.Application.Command.Interface;
using MoneyMe.Infrastructure.Data;
using MoneyMe.Infrastructure.Data.Interface;
using MoneyMe.Infrastructure.Data.Query;
using System;

namespace MoneyMe.API
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
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Program).Assembly);
            var connectionString = Configuration.GetSection("Database").GetValue<string>("ConnectionString");
            services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
            ApplicationDependency(services);
            InfrastructureDependency(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoneyMe");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ApplicationDependency(IServiceCollection services)
        {
            services.AddTransient<IAddCustomerCommand, AddCustomerCommand>();
        }

        private void InfrastructureDependency(IServiceCollection services)
        {
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }
    }
}
