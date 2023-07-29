using GSendShared;
using GSendShared.Abstractions;

using Shared.Classes;

namespace GSendCommon.MCodeOverrides
{
    internal class M605Override : IMCodeOverride
    {
        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            List<IGCodeCommand> m605Commands = overrideContext.GCode.Commands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode605)).ToList();

            if (m605Commands.Count == 0)
                return false;

            if (m605Commands.Count == 1)
            {
                IGCodeCommand soundCommand = m605Commands[0];

                string file = soundCommand.CommentStripped(true).Trim();

                if (File.Exists(file))
                {
                    PlaySoundThread playSoundThread = new PlaySoundThread(file, TimeSpan.Zero);
                    ThreadManager.ThreadStart(playSoundThread, $"Playing sound {file}", ThreadPriority.BelowNormal);

                    overrideContext.SendCommand = false;
                    return true;
                }
            }

            return false;
        }
    }
}
