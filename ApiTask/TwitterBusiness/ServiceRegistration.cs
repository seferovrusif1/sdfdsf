using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Repositories.Implements;
using Twitter.Business.Repositories.Interfaces;

namespace Twitter.Business
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<ITopicRepository,TopicRepository>();
            return service;
        }
    }
}
