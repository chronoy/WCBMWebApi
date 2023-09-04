

namespace Services.Web
{
    public class LifetimeEventsHostService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        public LifetimeEventsHostService(ILogger<LifetimeEventsHostService> logger,
                                         IHostApplicationLifetime appLifeTime)
        {
            _logger = logger;
            _appLifetime = appLifeTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void OnStarted()
        {
            //Start a backgroud service
            _logger.LogInformation("OnStarted has been called");
        }
        public void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called");
        }

        public void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called");
        }
    }
}
