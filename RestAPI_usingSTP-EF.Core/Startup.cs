using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestWithProc.Models;
//using TestWithProc.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace TestWithProc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
            .AddJwtBearer(options =>  
        {  
        options.TokenValidationParameters = new TokenValidationParameters  
        {  
            ValidateIssuer = true,  
            ValidateAudience = true,
            ValidateLifetime = true,  
            LifetimeValidator = CustomLifetimeValidator,
            ValidateIssuerSigningKey = true,  
            ValidIssuer = Configuration["Jwt:Issuer"],  
            ValidAudience = Configuration["Jwt:Issuer"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))  
        };  
    });

            services.AddDbContext<CustomerContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
//Swagger Cofig
            services.AddSwaggerGen(c => {  
            c.SwaggerDoc("v1", new Info {  
            Version = "v1",  
                Title = "TestWithProc",  
                Description = "ASP.NET Core Web API"  
            });  

            c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            loggerFactory.AddFile("Logs/TestWithProc-{Date}.txt");
            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            //app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();
//adding the Swagger UI to the Configure method, 
            app.UseSwagger();  
            app.UseSwaggerUI(c => {  
             c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWithProc"); 
            });
        }
    }
}







