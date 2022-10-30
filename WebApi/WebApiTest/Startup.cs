using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using IFramework.Api.Core.Extensions;
using IFramework.Domain.User;
using IFramework.Infra.Transversal.Log.NLog;
using IFramework.Infrastructure.Persistence.EFCore;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Logger;
using System.Collections.Generic;
using IFramework.Infrastructure.Utility.Extensions;
using IFramework.Infra.Transversal.IoC.CastleWindsor;
using IFramework.Infrastructure.Transversal.Mapper.AutoMapper;

namespace IFramework.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // custom services
            services.ConfigureSwagger(Configuration, "SwaggerOptions")
                .AddApiCore()
                .AddIFrameworkConfig(Configuration)
                .RegisterAutoMapper(typeof(AutoMappingProfile))
                .UseEFCore<EFDbContext>(Configuration)
                .ConfigureNLog(Configuration)
                .ConfigureRedis(Configuration)
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddMvc()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 
            return services.AddCastleIoC(new ContainerInstaller());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.ConfigureSwagger(Configuration, "SwaggerOptions");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "api/{controller}/{action}");
            });

            //SeedData();
        }

        protected void SeedData()
        {
            EFDbContext context = IoCResolver.Instance.ReleaseInstance<EFDbContext>();


            Role rolKullanici = new Role("Kullanıcı");
            List<AuthorizationList> autKullanici = new List<AuthorizationList>()
            {
                new AuthorizationList(){ Action="Select",Container="User" }
            };
            rolKullanici.AuthorizationListJson = autKullanici.ToJson();
            EntityEntry<Role> roleKullanici = context.Set<Role>().Add(rolKullanici);

            Role rolAdmin = new Role("Admin");
            List<AuthorizationList> autAdmin = new List<AuthorizationList>()
            {
                new AuthorizationList(){ Action="Select",Container="User" },
                new AuthorizationList(){ Action="Create",Container="User" },
                new AuthorizationList(){ Action="Update",Container="User" },
                new AuthorizationList(){ Action="Delete",Container="User" }
            };
            rolAdmin.AuthorizationListJson = autAdmin.ToJson();

            EntityEntry<Role> roleAdmin = context.Set<Role>().Add(rolAdmin);
            context.SaveChanges();
            User adminUser = new User("canerbaki@gmail.com", "1234-Baki");
            adminUser.ChangeUserInfo("Caner", "BAKİ", "resim1.jpg");
            adminUser.ChangeRole(context.Role.Find(roleAdmin.Entity.Id));

            EntityEntry<User> adminUserDb = context.Set<User>().Add(adminUser);
            adminUserDb.Entity.EmailApproved(adminUserDb.Entity.EmailApprovedCode, "127.0.0.1");
            context.SaveChanges();

            User deleteUser = new User("canerbakiDelete@gmail.com", "1234-Baki");
            deleteUser.ChangeUserInfo("Caner", "BAKİ", "resim1.jpg");
            deleteUser.ChangeRole(context.Role.Find(roleKullanici.Entity.Id));
            context.Set<User>().Add(deleteUser);
            context.SaveChanges();
            for (int i = 0; i < 3; i++)
            {
                User deleteMultipleUser = new User("canerbakiDelete" + i + "@gmail.com", "1234-Baki");
                deleteMultipleUser.ChangeUserInfo("Caner", "BAKİ", "resim1.jpg");
                deleteMultipleUser.ChangeRole(context.Role.Find(roleKullanici.Entity.Id));
                context.Set<User>().Add(deleteMultipleUser);
            }
            context.SaveChanges();
        }
    }
}
