using GSendShared.Interfaces;

namespace GSendTests.Mocks
{
    internal class MockCommonUtils : ICommonUtils
    {
        public bool GetGSendCS(out string gSendCS)
        {
            throw new System.NotImplementedException();
        }

        public void Sleep(int milliseconds)
        {
            // not used in this context
        }
    }
}
