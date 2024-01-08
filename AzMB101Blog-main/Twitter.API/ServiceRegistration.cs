using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Twitter.Core.Entities;
using Twitter.DAL.Contexts;
using Twitter.Business.Services.Implements;
using Twitter.Business.Services.Interfaces;

namespace Twitter.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddUserIdentity(this IServiceCollection service)
        {

            service.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<TwitterContext>();
            return service;
        }
    }
}

