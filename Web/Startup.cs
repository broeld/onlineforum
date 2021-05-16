using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DAL.Infrastructure;
using AutoMapper;
using DAL.Infrastructure.Services;
using BLL.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.Automapper;
using Web.Middlewares;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            var connection_string = Configuration.GetValue<string>("DefaultConnection");

            services.RegisterDataServices("Host=localhost; Port=5432; Database=Forum; User ID=postgres; Password=dinadina; Pooling=true;"); //Add connection string;

            services.RegisterBusinessServices();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") //отут
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(auth =>
            {
              auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtBearerOptions => 
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Tokens:Audience"],
                        ValidateLifetime = bool.Parse(Configuration["Tokens:ValidateLifetime"]),
                        ClockSkew = TimeSpan.FromMinutes(int.Parse(Configuration["Tokens:ExpiryMinutes"]))
                    };
                });

            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutomapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum");
             });

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            // app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
