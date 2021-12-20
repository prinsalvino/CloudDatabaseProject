using DAL;
using DAL.RepoInterfaces;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;
using Service.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace OrderQueue
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(Configure)
                .Build();

            host.Run();
        }

        private static void Configure(HostBuilderContext Builder, IServiceCollection Services)
        {
            Services.AddDbContext<ShoppingContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection")));
            Services.AddTransient<IOrderService, OrderService>();
            Services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}