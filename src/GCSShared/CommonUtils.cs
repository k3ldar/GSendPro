using System.Reflection;

using GSendShared.Interfaces;

namespace GSendShared
{
    internal class CommonUtils : ICommonUtils
    {
        public void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public bool GetGSendCS(out string gSendCS)
        {
#if DEBUG
            gSendCS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("-windows", String.Empty), "GSendCS.exe");
#else
            gSendCS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GSendCS.exe");

#endif
            if (File.Exists(gSendCS))
                return true;

            return false;
        }
    }
}
