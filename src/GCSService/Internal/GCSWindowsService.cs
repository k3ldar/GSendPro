using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using GSendShared;

using Microsoft.Extensions.Hosting;

using PluginManager.Abstractions;

using Shared.Classes;

using ILogger = PluginManager.Abstractions.ILogger;

namespace GSendService.Internal
{
    [ExcludeFromCodeCoverage]
    public class GCSWindowsService : BackgroundService
    {
        #region Private Members

        private readonly ILogger _logger;
        private readonly IProcessorMediator _processorMediator;

        #endregion Private Members

        public GCSWindowsService(ILogger logger, IProcessorMediator processorMediator)
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
