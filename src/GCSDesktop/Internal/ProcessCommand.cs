using System;

using GSendApi;

using GSendDesktop.Abstractions;

namespace GSendDesktop.Internal
{
    internal class CommandProcessor : ICommandProcessor
    {
        private readonly IMessageNotifier _messageNotifier;

        public CommandProcessor(IMessageNotifier messageNotifier)
        {
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));
        }

        public void ProcessCommand(Action command)
        {
            try
            {
                command();
            }
            catch (AggregateException)
            {
                // ignore
            }
            catch (GSendApiException gs)
            {
                _messageNotifier.ShowMessage(gs.Message);
            }
            catch (Exception ex)
            {
                _messageNotifier.ShowMessage(ex.Message);
            }
        }

    }
}
