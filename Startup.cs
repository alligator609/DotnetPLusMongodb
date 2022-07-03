using DotnetPLusMongodb.models;
using DotnetPLusMongodb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetPLusMongodb
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetPLusMongodb", Version = "v1" });
            });
            // add servics to container
            // get the database name from appsettings and map them to  StudentStoreDatabaseSettings class
            services.Configure<StudentStoreDatabaseSettings>(
                Configuration.GetSection(nameof(StudentStoreDatabaseSettings)
                ));
            // below line tie IStudentStoreDatabaseSettings to StudentStoreDatabaseSettings, so whenever instance of IStudentStoreDatabaseSettings
            // require then provide instance of this StudentStoreDatabaseSettings. in StudentService class we are injecting
            // IStudentStoreDatabaseSettings so we get the configuration info from StudentStoreDatabaseSettings
            services.AddSingleton<IStudentStoreDatabaseSettings>(sp=> sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>> ().Value);
            // tell mongo client where he has to read form and in our case that would be StudentStoreDatabaseSettings:ConnectionString
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));
            // and at last tie IStudentService to its emplementation StudentService, check StudentService class which implements IStudentService
            services.AddScoped<IStudentService, StudentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetPLusMongodb v1"));
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
