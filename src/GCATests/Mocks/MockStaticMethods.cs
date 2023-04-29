using GSendShared.Interfaces;

namespace GSendTests.Mocks
{
    internal class MockStaticMethods : IStaticMethods
    {
        public void Sleep(int milliseconds)
        {
            // not used in this context
        }
    }
}
