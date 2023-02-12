using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using PluginManager.Abstractions;

namespace GCSShared
{
    public class GCSWindowsService : BackgroundService
    {
        #region Private Members

        private readonly ILogger _logger;

        #endregion Private Members

        public GCSWindowsService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Overridden Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.AddToLog(PluginManager.LogLevel.Information, "SmokeTest Background Scheduler Starting");
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    await Task.Delay(250, stoppingToken);
                }
            }
            finally
            {

                _logger.AddToLog(PluginManager.LogLevel.Information, "SmokeTest Background Scheduler Stopping");
            }
        }

        #endregion Overridden Methods
    }
}
