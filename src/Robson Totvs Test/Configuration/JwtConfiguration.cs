using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Robson_Totvs_Test.Configuration
{
    public static class Configuration
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtAppSettingsSection = config.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtAppSettingsSection);

            var myJwtSettings = jwtAppSettingsSection.Get<JwtSettings>();
            var myKeyInBytes = Encoding.UTF8.GetBytes(myJwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidAudience = myJwtSettings.ValidAt,
                        ValidIssuer = myJwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(myKeyInBytes)
                    };
                });

            services.AddAuthorization(AuthorizationConfiguration);
        }

        private static void AuthorizationConfiguration(AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            var myJwtPolicy = policyBuilder.RequireAuthenticatedUser()
                .Build();

            options.DefaultPolicy = myJwtPolicy;
        }
    }
}
