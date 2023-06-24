using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PluginManager.Abstractions;

namespace GSendControls
{
    public static class GlobalExceptionHandler
    {
        private static ILogger _logger;

        public static void InitializeGlobalExceptionHandlers(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.AddToLog(PluginManager.LogLevel.Error, (Exception)e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _logger.AddToLog(PluginManager.LogLevel.Error, e.Exception);
        }
    }
}
