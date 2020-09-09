using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Application.Interface;
using Travel.Presistance.Services;

namespace Travel.Presistance
{
    public static  class DependencyInjectionBase
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseInMemoryDatabase("CleanArchitectureDb"));
            //}
            //else
            //{
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(
                    configuration.GetConnectionString("TravelDb"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                }
               
                       );
            //}

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

         //   services.AddIdentityServer()
         //.AddApiAuthorization<User, ApplicationDbContext>();

            services.AddAuthentication()
           .AddIdentityServerJwt();


            return services;
        }


    }
}
