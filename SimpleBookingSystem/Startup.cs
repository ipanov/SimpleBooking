using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBookingSystem.DataAccessLayer;
using MediatR;
using System.Reflection;
using SimpleBookingSystem.Business.Handlers;
using SimpleBookingSystem.Business.Commands;
using SimpleBookingSystem.Business.Validators;
using SimpleBookingSystem.Business.Services;
using SimpleBookingSystem.Business.Emailer;

namespace SimpleBookingSystem
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

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
            services.AddSwaggerGen();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRequestHandler<SaveBookingCommand, bool>, SaveBookingCommandHandler>();
            services.AddScoped<IBookingValidator, BookingValidator>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IResourcesService, ResourcesService>();

            services.AddDbContext<SimpleBookingSystemDbContext>(
                options => options.UseSqlite("name=ConnectionStrings:SimpleBookingSystemDbConnection"));

           
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SimpleBookingSystemDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

         
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
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
