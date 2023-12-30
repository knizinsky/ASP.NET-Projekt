using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GroceryStore.Models;

namespace GroceryStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services
                .AddIdentity<ApplicationUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedAccount = true
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            if (userManager.FindByEmailAsync("admin@example.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser { Email = "admin@example.com", };

                if (!roleManager.RoleExistsAsync("Administrator").Result)
                {
                    IdentityRole role = new IdentityRole { Name = "Administrator" };
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                    if (roleResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                    endpoints.MapRazorPages();
                }
            );
        }
    }
}
