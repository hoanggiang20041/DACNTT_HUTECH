using Microsoft.EntityFrameworkCore;
using Chamsoc.Data;

namespace Chamsoc
{
    public static class DatabaseSetup
    {
        public static void SetupDatabase(string connectionString)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 44))
                );

                using var context = new AppDbContext(optionsBuilder.Options);
                
                // Đảm bảo database được tạo
                context.Database.EnsureCreated();
                
                Console.WriteLine("✓ Database setup completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error setting up database: {ex.Message}");
                Console.WriteLine("Please run the following SQL commands manually:");
                Console.WriteLine("============================================");
                Console.WriteLine("CREATE DATABASE IF NOT EXISTS chamsoc CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
                Console.WriteLine("GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'localhost';");
                Console.WriteLine("GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'%';");
                Console.WriteLine("FLUSH PRIVILEGES;");
                Console.WriteLine("============================================");
                throw;
            }
        }

        public static bool TestConnection(string connectionString)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 44))
                );

                using var context = new AppDbContext(optionsBuilder.Options);
                context.Database.CanConnect();
                
                Console.WriteLine("✓ Database connection successful!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}

