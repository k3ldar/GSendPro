namespace GSendShared.Interfaces
{
    public interface ICommonUtils
    {
        public bool GetGSendCS(out string gSendCS);

        void Sleep(int milliseconds);
    }
}
