using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GroceryStore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracje relacji i innych ustawień
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(o => o.Products)
                .UsingEntity(j => j.ToTable("OrderProducts"));
        }

        // Klasa fabryki kontekstu dla migracji
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);

                return new ApplicationDbContext(builder.Options);
            }
        }

        // Metoda do tworzenia konta administratora
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@example.com";
            var adminName = "admin";
            var adminPassword = "Password123!";

            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminName,
                    Email = adminEmail,
                    Password = adminPassword,
                };

                userManager.CreateAsync(adminUser, adminPassword).Wait();
                var roleExists = roleManager.RoleExistsAsync("Administrator").Result;

                if (!roleExists)
                {
                    // Jeśli rola nie istnieje, utwórz ją
                    roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
                }

                // Przypisz użytkownikowi rolę "Administrator"
                var token = userManager.GenerateEmailConfirmationTokenAsync(adminUser).Result;
                userManager.ConfirmEmailAsync(adminUser, token).Wait();
                userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
            }
        }

        public static void SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Initialize(context, userManager, roleManager);

            // Dodaj kod seedowania tutaj
            var adminUser = userManager.FindByEmailAsync("admin@example.com").Result;

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    Password = "Password123!", // Ustaw hasło tutaj
                };

                // Użyj PasswordHasher do zahashowania hasła
                var passwordHash = userManager.PasswordHasher.HashPassword(adminUser, adminUser.Password);
                adminUser.PasswordHash = passwordHash;

                userManager.CreateAsync(adminUser).Wait();

                // Sprawdź, czy rola "Administrator" istnieje
                var roleExists = roleManager.RoleExistsAsync("Administrator").Result;

                if (!roleExists)
                {
                    // Jeśli rola nie istnieje, utwórz ją
                    roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
                }

                var token = userManager.GenerateEmailConfirmationTokenAsync(adminUser).Result;
                userManager.ConfirmEmailAsync(adminUser, token).Wait();
                userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
            }
        }


    }
}
