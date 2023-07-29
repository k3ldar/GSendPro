using System.Media;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class PlaySoundThread : ThreadManager
    {
        public PlaySoundThread(string soundFile, TimeSpan runInterval, ThreadManager parent = null, int delayStart = 0, int sleepInterval = 200, bool runAtStart = true, bool monitorCPUUsage = true)
            : base(soundFile, runInterval, parent, delayStart, sleepInterval, runAtStart, monitorCPUUsage)
        {
            ContinueIfGlobalException = false;
            HangTimeout = TimeSpan.FromMinutes(2).Milliseconds;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Will find another way for other platforms when needed")]
        protected override bool Run(object parameters)
        {
            string soundFile = parameters.ToString();

            if (File.Exists(soundFile))
            {
                using SoundPlayer player = new SoundPlayer(soundFile);
                player.Play();

                while (!player.IsLoadCompleted)
                {
                    Thread.Sleep(0);
                }
            }

            return false;
        }
    }
}
