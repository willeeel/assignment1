using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public enum Level
        {
            Low,
            Normal,
            High
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("The service has been stopped.");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Random rnd = new Random();
                int temperature = rnd.Next(1, 101);
                var _tempLevel = Level.Normal;

                switch(temperature)
                {
                    case int t when t < 50:
                        _tempLevel = Level.Low;
                        _logger.LogInformation($"Temperature is: {temperature},  low");
                        break;

                    case int t when t  >50:
                            _tempLevel = Level.High;
                        _logger.LogInformation($"Temperature is: {temperature},  high");
                        break;
                }
                
                await Task.Delay(60*1000, stoppingToken);

            }
        }
    }
}
