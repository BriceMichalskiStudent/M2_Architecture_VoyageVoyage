using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FirebaseAdmin;

using Google.Apis.Auth.OAuth2;

using MicroService.User.Service.Declaration;
using MicroService.User.Service.Repository;
using MicroService.User.Service.Service;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MicroService.Travel.WebApi
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
            FirebaseApp.Create(new AppOptions()
                                   {
                                       Credential = GoogleCredential.FromFile("./userwebapi.json"),
                                   });
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.Authority = "https://securetoken.google.com/userwebapi-6e989";
                        options.TokenValidationParameters = new TokenValidationParameters
                                                                {
                                                                    ValidateIssuer = true,
                                                                    ValidIssuer = "https://securetoken.google.com/userwebapi-6e989",
                                                                    ValidateAudience = true,
                                                                    ValidAudience = "userwebapi-6e989",
                                                                    ValidateLifetime = true
                                                                };
                    });
            services.AddScoped<ITravelRepository, TravelRepository>();
            services.AddScoped<ITravelService, TravelService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
