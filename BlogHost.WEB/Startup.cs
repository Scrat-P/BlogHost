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

namespace BlogHost.WEB
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("BlogHost.DAL")));

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();

            // Add application repositories.
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAutoMapper();

            Mapper.Initialize(cfg => cfg.AddProfiles(
                Assembly.GetAssembly(typeof(PostTagDTOProfile)),
                Assembly.GetAssembly(typeof(PostTagVMProfile)),
                Assembly.GetAssembly(typeof(TagDTOProfile)),
                Assembly.GetAssembly(typeof(TagVMProfile)),
                Assembly.GetAssembly(typeof(LikeDTOProfile)),
                Assembly.GetAssembly(typeof(LikeVMProfile)),
                Assembly.GetAssembly(typeof(CommentDTOProfile)),
                Assembly.GetAssembly(typeof(CommentVMProfile)),
                Assembly.GetAssembly(typeof(PostDTOProfile)),
                Assembly.GetAssembly(typeof(PostVMProfile)),
                Assembly.GetAssembly(typeof(BlogDTOProfile)),
                Assembly.GetAssembly(typeof(BlogVMProfile)),
                Assembly.GetAssembly(typeof(UserDTOProfile)),
                Assembly.GetAssembly(typeof(UserVMProfile)),
                Assembly.GetAssembly(typeof(Startup))));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
