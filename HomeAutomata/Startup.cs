using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.MySql;
using HomeAutomata.Data;
using HomeAutomata.Data.Repositories;
using HomeAutomata.Data.Services;
using HomeAutomata.Hangfire.RecurringJobs;
using HomeAutomata.Services.HeatPump;
using HomeAutomata.Services.HttpServices.Weather;
using HomeAutomata.Services.Weather;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Transactions;

namespace HomeAutomata
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(o =>
            //    o.UseMySql(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        sql => sql.ServerVersion(new ServerVersion(new Version(10, 3, 22), ServerType.MariaDb))));

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("data.db"));

                services.AddHangfire(configuration => configuration
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage());
            }
            else
            {
                services.AddDbContext<AppDbContext>(o =>
                o.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.ServerVersion(new ServerVersion(new Version(10, 3, 22), ServerType.MariaDb))));

                services.AddHangfire(configuration => configuration
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseStorage(new MySqlStorage(Configuration.GetConnectionString("DefaultConnection"), new MySqlStorageOptions
                    {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 50000,
                        TransactionTimeout = TimeSpan.FromMinutes(1),
                        TablesPrefix = "Hangfire"
                    })));
            }

            services.AddScoped(typeof(IRepo<>), typeof(BaseRepo<>));
            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddScoped<IOutsideWeatherService, OutsideWeatherService>();

            services.AddScoped<ILogOutsideWeatherJob, LogOutsideWeatherJob>();
            services.AddScoped<IHeatPumpService, HeatPumpService>();
            services.AddScoped<IHeatPumpConsumptionService, HeatPumpConsumptionService>();

            services.AddHangfireServer();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<ILogOutsideWeatherJob>(j => j.LogWeather(), Cron.Hourly, TimeZoneInfo.Local);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}