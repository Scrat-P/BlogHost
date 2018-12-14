using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHost.DAL.Data;
using BlogHost.DAL.Entities;
using BlogHost.DAL.Repositories;
using BlogHost.DAL.RepositoryInterfaces;
using BlogHost.BLL.Services;
using BlogHost.BLL.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Reflection;
using BlogHost.BLL.Mappers;
using BlogHost.WEB.Models.MappingProfiles;
using BlogHost.WEB.Hubs;
using BlogHost.WEB.Areas.Identity.Services;
using BlogHost.WEB.RouteConstraints;

namespace BlogHost.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("BlogHost.DAL")));

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();
            
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            Mapper.Initialize(cfg => cfg.AddProfiles(
                Assembly.GetAssembly(typeof(UserDTOProfile)),
                Assembly.GetAssembly(typeof(UserVMProfile))));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<LikeHub>("/like");
                routes.MapHub<CommentHub>("/comment");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "alias",
                    template: "{*clientRoute}",
                    defaults: new { controller = "Blog", action = "Show" },
                    constraints: new { clientRoute = new TitleConstraint() }
                );
                //routes.MapSpaFallbackRoute(
                //    name: "alias",
                //    defaults: new { controller = "Blog", action = "Show" }
                //);
            });
        }
    }
}
