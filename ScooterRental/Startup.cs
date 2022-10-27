using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScooterRental.Core.Calculators;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using ScooterRental.Data;
using ScooterRental.Services;

namespace ScooterRental
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScooterRental", Version = "v1" });
            });

            services.AddDbContext<ScooterRentalDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("scooter-rental"));
            });

            services.AddScoped<IScooterRentalDbContext, ScooterRentalDbContext>();
            services.AddScoped<IDbService, DbService>();
            //
            services.AddScoped<IEntityService<Scooter>, EntityService<Scooter>>();
            services.AddScoped<IEntityService<RentalReport>, EntityService<RentalReport>>();
            //services.AddScoped<IEntityService<IncomeReport>, EntityService<IncomeReport>>();
            //
            services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());
            //
            services.AddScoped<IScooterService, ScooterService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRentalService, RentalService>();
            //
            services.AddScoped<IRentalIncomeCalculator, RentalIncomeCalculator>();
            services.AddScoped<IScooterValidator, ScooterPriceValidator>();
            services.AddScoped<IScooterValidator, ScooterStatusValidator>();
            services.AddScoped<IScooterValidator, ScooterPropertyValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScooterRental v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
