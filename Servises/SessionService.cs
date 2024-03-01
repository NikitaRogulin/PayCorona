using PayCorona.Interface;

namespace PayCorona.Servises
{
    public class SessionService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public SessionService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope()) 
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<ISessionRepository>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    scopedService.DeleteExpiredSessions();
                    await Task.Delay(_interval, stoppingToken);
                }
            }
                
        }
    }
}
