using GSendShared.Abstractions;

namespace GSendCommon
{
    public sealed class StaticMethods : IStaticMethods
    {
        public void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
