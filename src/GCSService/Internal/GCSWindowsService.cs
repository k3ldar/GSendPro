using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using GSendShared;

using Microsoft.Extensions.Hosting;

using ILogger = PluginManager.Abstractions.ILogger;

namespace GSendService.Internal
{
    [ExcludeFromCodeCoverage]
    public class GcsWindowsService : BackgroundService
    {
        #region Private Members

        private readonly ILogger _logger;
        private readonly IProcessorMediator _processorMediator;

        #endregion Private Members

        public GcsWindowsService(ILogger logger, IProcessorMediator processorMediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _processorMediator = processorMediator ?? throw new ArgumentNullException(nameof(processorMediator));
        }

        #region Overridden Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.AddToLog(PluginManager.LogLevel.Information, "GSend Background Service Starting");
            _processorMediator.OpenProcessors();
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(250, stoppingToken);
                }
            }
            finally
            {
                _processorMediator.CloseProcessors();
                _logger.AddToLog(PluginManager.LogLevel.Information, "GSend Background Service Stopping");
            }
        }

        #endregion Overridden Methods


    }
}
