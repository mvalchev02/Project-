using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace UniSpace
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���������� �� ��� �� ������ ��� ������ �����
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // �������� �� Razor Pages � Controllers
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            // ��������� �� Identity � ���� Admin � Professor
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var app = builder.Build();

            // ������������� �� HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // ��������� �� ���� � ����������� ��� ���������� �� ������������
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                await CreateRoles(roleManager, userManager);
            }

            // Middleware pipeline setup
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        // Seed roles � ��������� �� �����������
        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            string[] roleNames = { "Admin", "Professor" };

            // �������� � ��������� �� ����
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // ��������� �� default �������������
            string adminEmail = "admin@myuni.com";
            string adminPassword = "Admin123321";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // ��������� �� ��������
            string professorEmail = "karpunchev@myuni.com";
            string professorPassword = "Professor@123";

            var professorUser = await userManager.FindByEmailAsync(professorEmail);
            if (professorUser == null)
            {
                professorUser = new IdentityUser
                {
                    UserName = professorEmail,
                    Email = professorEmail
                };

                var result = await userManager.CreateAsync(professorUser, professorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(professorUser, "Professor");
                }
            }
        }
    }
}
