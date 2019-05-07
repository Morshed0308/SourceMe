using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SourceMe.Storage.Data;
using SourceMe.Storage.Data.Entities;
using SourceMeWebApp.Services;

namespace SourceMeWebApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config,IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddIdentity<StoreUser,IdentityRole >(cfg =>
              {
                  cfg.User.RequireUniqueEmail = true;
  

              })
                .AddEntityFrameworkStores<SourceMeContext>();

           // services.AddIdentity<StoreUser, IdentityOptions>();

            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:key"]))

                    };

                }

                );

            services.AddDbContext<SourceMeContext>(cfg => {
                cfg.UseSqlServer(_config.GetConnectionString("SourceMeConnectionString"));
            });

            services.AddScoped<IRssService, RssService>();
            services.AddScoped<TestSeeder>();

            services.AddScoped<ICategoryService, CategoryService>();
            // configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // add iis 
            services.Configure<IISOptions>(options =>
            {

            });
            

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(cfg => 
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    
                    var seeder = scope.ServiceProvider.GetService<TestSeeder>();
                    seeder.seed().Wait();
                }
            }


        }
    }
}
