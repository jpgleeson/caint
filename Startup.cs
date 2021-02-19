using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace caint
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
            //services.AddDbContext<CommentContext>(opt => opt.UseInMemoryDatabase("caintcomments"));
            //services.AddDbContext<ThreadContext>(opt => opt.UseInMemoryDatabase("caintthreads"));

            //SQLITE
            services.AddDbContext<caintDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("caintDBContext")));

            /*services.AddDbContextPool<CommentContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        // Replace with your connection string.
                        Configuration.GetConnectionString("caintDb"),
                        // Replace with your server version and type.
                        // For common usages, see pull request #1233.
                        new MySqlServerVersion(new Version(8, 0, 21))
            ));

            services.AddDbContextPool<ThreadContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        // Replace with your connection string.
                        Configuration.GetConnectionString("caintDb"),
                        // Replace with your server version and type.
                        // For common usages, see pull request #1233.
                        new MySqlServerVersion(new Version(8, 0, 21))
            ));*/
            

            

            services.AddCors(options =>
            {
            options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
