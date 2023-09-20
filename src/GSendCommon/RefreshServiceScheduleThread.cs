using GSendShared;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class RefreshServiceScheduleThread : ThreadManager
    {
        public RefreshServiceScheduleThread(IUiUpdate uiUpdate, ThreadManager parentThread)
            : base(uiUpdate, TimeSpan.Zero, parentThread)
        {
            HangTimeout = 0;
            ContinueIfGlobalException = true;
        }

        protected override bool Run(object parameters)
        {
            IUiUpdate uiUpdate = (IUiUpdate)parameters;

            uiUpdate.RefreshServiceSchedule();
            return false;
        }
    }
}
