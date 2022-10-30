using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace IFramework.Infrastructure.Transversal.Mapper.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services,params Type[] profiles)
        {
            List<Type> profileTypes=profiles.ToList();
            profileTypes.Add(typeof(AutoMappingProfile));
            services.AddAutoMapper(profileTypes.ToArray());
            return services;
        }
    }
}
