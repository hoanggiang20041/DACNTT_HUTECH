using Chamsoc.Data;
using Chamsoc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Chamsoc.Hubs;
using Chamsoc.Services;
using Microsoft.Extensions.Logging;
using Chamsoc;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ
builder.Services.AddControllersWithViews();

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                try
                {
                    var host = new Uri(origin).Host;
                    return
                        origin == "https://localhost:7198" ||
                        origin == "http://localhost:5271" ||
                        origin == "https://3acb-183-80-94-205.ngrok-free.app" ||
                        origin == "https://chamsoc-gkm.loca.lt" ||
                        host.EndsWith(".trycloudflare.com", StringComparison.OrdinalIgnoreCase) ||
                        host.EndsWith(".ngrok-free.app", StringComparison.OrdinalIgnoreCase) ||
                        host.EndsWith(".loca.lt", StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Cấu hình DbContext (MariaDB/MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 44)),
        mySqlOptionsAction: mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }
    ));

// Cấu hình Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configure cookie policy
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
});

builder.Services.AddHttpClient<OpenRouterService>();
builder.Services.AddScoped<OpenRouterService>();
builder.Services.AddSingleton<Chamsoc.Services.EmailService>();

// Thêm Jitsi Meet Service
builder.Services.AddSingleton<JitsiMeetService>();

// Thêm MiroTalk P2P Service
builder.Services.AddSingleton<MiroTalkMeetService>();

// Thêm SignalR
builder.Services.AddSignalR();

// Thêm logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Debug);
});

var app = builder.Build();

// Thiết lập database trước khi chạy ứng dụng
try
{
    var connectionString = app.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString))
    {
        app.Logger.LogInformation("Setting up database...");
        DatabaseSetup.SetupDatabase(connectionString);
        
        app.Logger.LogInformation("Testing database connection...");
        if (DatabaseSetup.TestConnection(connectionString))
        {
            app.Logger.LogInformation("Database connection successful!");
        }
    }
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Database setup failed. Please check MySQL is running and credentials are correct.");
    app.Logger.LogError("You may need to manually run the SQL commands from fix_mysql.sql file");
}

// Tạo tài khoản Admin và vai trò
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        if (!await roleManager.RoleExistsAsync("Senior"))
        {
            await roleManager.CreateAsync(new IdentityRole("Senior"));
        }
        if (!await roleManager.RoleExistsAsync("Caregiver"))
        {
            await roleManager.CreateAsync(new IdentityRole("Caregiver"));
        }

        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            app.Logger.LogInformation("Bắt đầu tạo tài khoản Admin...");
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gkmcare.com",
                PhoneNumber = "0123456789",
                Role = "Admin",
                RoleId = "0",
                IsLocked = false,
                Balance = 0,
                FullName = "Administrator",
                Address = "GKM Care Office",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GKMCARE.COM"
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                app.Logger.LogInformation("Tạo tài khoản Admin thành công!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
                app.Logger.LogInformation("Gán vai trò Admin thành công!");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                app.Logger.LogError("Lỗi khi tạo tài khoản Admin: {Errors}", errors);
                throw new Exception("Không thể tạo tài khoản Admin: " + errors);
            }
        }
        else
        {
            app.Logger.LogInformation("Tài khoản Admin đã tồn tại.");
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Lỗi khi tạo tài khoản Admin");
    }
}

// Cấu hình pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// Cho phép quyền thiết bị trong iframe cùng nguồn (Permissions-Policy)
app.Use(async (context, next) =>
{
    context.Response.Headers["Permissions-Policy"] = string.Join(
        ", ",
        new[]
        {
            "camera=*",
            "microphone=*",
            "fullscreen=*",
            "autoplay=*",
            "display-capture=*",
            "encrypted-media=*",
            "accelerometer=*",
            "gyroscope=*",
            "clipboard-write=*"
        }
    );
    await next();
});
app.UseRouting();

// Áp dụng CORS trước authentication và authorization
app.UseCors("AllowSpecificOrigins"); // Chỉ dùng chính sách đã định nghĩa

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<CallHub>("/callHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=ChamSocSucKhoe}/{id?}");

app.Logger.LogInformation("Server khởi động. Lắng nghe tại https://localhost:7198");

app.Run();