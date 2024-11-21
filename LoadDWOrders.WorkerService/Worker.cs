using LoadDWOrders.Data.Interfaces;

namespace LoadDWOrders.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dataService = scope.ServiceProvider.GetRequiredService<IDataServiceDWOrders>();
                        try
                        {
                            var result = await dataService.LoadDw();
                            if (!result.Success)
                            {
                                _logger.LogError(result.Message);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred while loading data.");
                        }
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
