using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Robson_Totvs_Test.Configuration;
using Robson_Totvs_Test.Configuration.TokenService;
using Robson_Totvs_Test.Data;
using Robson_Totvs_Test.Data.Repositories;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Domain.Interfaces.Repositories;
using Robson_Totvs_Test.Domain.Interfaces.Services;

namespace Robson_Totvs_Test
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Robson_Totvs_Test", Version = "v1" });
            });

            ConfigureDatabase(services);
            ConfigureHttpPostNameToLowerCase(services);
            services.ConfigureJwtAuthentication(this.Configuration);

            ConfigureDependencyInjections(services);
        }

        private void ConfigureDependencyInjections(IServiceCollection services)
        {
            services.AddScoped<ITotvsTokenService, TotvsTokenService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProfileObjectRepository, ProfileObjectRepository>();
        }

        private void ConfigureHttpPostNameToLowerCase(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var settings = options.SerializerSettings;
                settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.NullValueHandling = NullValueHandling.Include;
                settings.DefaultValueHandling = DefaultValueHandling.Include;
                settings.Formatting = Formatting.Indented;
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<TotvsTestDbContext>(options =>
            {
                options.UseNpgsql(this.Configuration.GetConnectionString("TotvsConnection"))
                     .UseLowerCaseNamingConvention();
            });

            services.AddDefaultIdentity<Account>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<TotvsTestDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Robson_Totvs_Test v1"));
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
