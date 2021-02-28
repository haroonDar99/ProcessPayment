using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProcessPayment.DataLayer;
using ProcessPayment.Helpers;
using ProcessPayment.Repository;

namespace ProcessPayment
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
      // Services (Repository) Dependency Injection
      services.AddScoped<IRepository, myRepository>();

      //DbModels Dependency Injection
      services.AddControllersWithViews();
      services.AddDbContext<PaymentsDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("BusinessDatabase")));

      services.AddCors(c =>
      {
        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
      });

      services.AddControllers();

      //Swagger
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments API", Description = "Payments API Swagger", Version = "v1" });
      });

      // Auto Mapper Configurations
      //var mapperConfig = new MapperConfiguration(mc =>
      //{
      //  mc.AddProfile(new MappingProfile());
      //});
      //IMapper mapper = mapperConfig.CreateMapper();

      services.AddAutoMapper(typeof(Startup));
      //
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentsDbContext db)
    {
      app.UseCors("AllowOrigin");

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //Swagger
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.DefaultModelsExpandDepth(-1); // Hide the SCHEMAS
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments API v1");
      });
      //

      //Db creation with execution
      //db.Database.EnsureCreated();

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
