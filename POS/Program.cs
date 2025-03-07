using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using POS.Repository.Context;
using POS.Core.Repository;
using POS.Core.Service;
using POS.Repository;
using POS.Service;

namespace POS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting POS application..."); //Testing for PR
            // Create a Host to manage services
            var host = CreateHostBuilder(args).Build();

            // Run database migrations
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<POSDbContext>();
                    Console.WriteLine("Applying migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Load configuration
                    var configuration = context.Configuration;

                    // Register repository layer (removes direct dependency on DbContext)
                    services.AddDbContext<POSDbContext>(options =>
                    options.UseSqlServer("Data Source=DESKTOP-J5IS95J\\SQLEXPRESS;Initial Catalog=POS_Updated;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

                    // AutoMapper
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                    // Repository Layer
                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<ISaleRepository, SaleRepository>();
                    services.AddScoped<ISaleItemRepository, SaleItemRepository>();
                    services.AddScoped<IDiscountRepository, DiscountRepository>();
                    services.AddScoped<ITaxRepository, TaxRepository>();
                    services.AddScoped<IReceiptRepository, ReceiptRepository>();
                    services.AddScoped<IPaymentRepository, PaymentRepository>();

                    // Service Layer
                    services.AddScoped<IProductService, ProductService>();
                    services.AddScoped<ISaleService, SaleService>();
                    services.AddScoped<ISaleItemService, SaleItemService>();
                    services.AddScoped<IDiscountService, DiscountService>();
                    services.AddScoped<ITaxService, TaxService>();
                    services.AddScoped<IReceiptService, ReceiptService>();
                    services.AddScoped<IPaymentService, PaymentService>();
                });
    }
}
