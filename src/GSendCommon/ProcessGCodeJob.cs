using GSendShared;

using Shared.Classes;

namespace GSendCommon
{
    internal sealed class ProcessGCodeJob : ThreadManager
    {
        private readonly GCodeProcessor _processor;

        public ProcessGCodeJob(GCodeProcessor gCodeProcessor)
            : base(null, TimeSpan.FromMilliseconds(Constants.QueueProcessMilliseconds), gCodeProcessor)
        {
            _processor = gCodeProcessor ?? throw new ArgumentNullException(nameof(gCodeProcessor));
            base.HangTimeout = 0;
            base.ContinueIfGlobalException = true;
        }

        protected override bool Run(object parameters)
        {
            _processor.ProcessNextCommand();

            return !HasCancelled();
        }
    }
}
