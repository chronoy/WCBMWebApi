

using Models;
namespace Services.Web
{
    public class LifetimeEventsHostService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IPDBService _PDBService;
        private readonly IOPCClientService _OPCService;
        public LifetimeEventsHostService(ILogger<LifetimeEventsHostService> logger,
                                         IHostApplicationLifetime appLifeTime,
                                         IPDBService pdbService,
                                         IOPCClientService OPCClientService)
        {
            _logger = logger;
            _appLifetime = appLifeTime;
            _PDBService = pdbService;  
            _OPCService = OPCClientService;             
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

            _OPCService.SetOPCItems(_PDBService.GetAllPDBTags().ToList());
            _OPCService.Run();
            _PDBService.Run();
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
