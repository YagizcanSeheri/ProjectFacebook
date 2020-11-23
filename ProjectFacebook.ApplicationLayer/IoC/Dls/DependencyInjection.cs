using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProjectFacebook.ApplicationLayer.AutoMapper;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.IoC.Dls
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedAccount = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.User.RequireUniqueEmail = true;
                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
