using LoadDWOrders.Data.Context;
using LoadDWOrders.Data.Interfaces;
using LoadDWOrders.Data.Services;
using LoadDWOrders.WorkerService;
using Microsoft.EntityFrameworkCore;



internal class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {

            services.AddDbContextPool<NorwindContext>(options => options
            .UseSqlServer(hostContext.Configuration.GetConnectionString("NorwindConnection"), sqlOptions =>
                sqlOptions.EnableRetryOnFailure()));


            services.AddDbContextPool<DWOrdersContext>(options => options
            .UseSqlServer(hostContext.Configuration.GetConnectionString("DWOrdersConnection"), sqlOptions =>
                sqlOptions.EnableRetryOnFailure()));

            services.AddScoped<IDataServiceDWOrders, DataServiceDWOrders>();

            services.AddHostedService<Worker>();
        });
 }
