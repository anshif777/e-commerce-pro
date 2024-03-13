namespace e_commerce_pro.Services
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using e_commerce_pro.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class OtpCleanupService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public OtpCleanupService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDb>();

                    // Remove expired OTP records
                    var expiredOtps = dbContext.OTPinfo.Where(x => x.expertime <= DateTime.UtcNow).ToList();
                    dbContext.OTPinfo.RemoveRange(expiredOtps);
                    dbContext.SaveChanges();
                }

                // Sleep for a specified interval before running the task again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
