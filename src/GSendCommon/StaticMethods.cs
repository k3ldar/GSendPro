using GSendShared.Abstractions;

namespace GSendCommon
{
    public sealed class StaticMethods : IStaticMethods
    {
        public void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            //DateTime endMilliseconds = DateTime.UtcNow.AddMilliseconds(milliseconds);

            //while (endMilliseconds.Millisecond < DateTime.UtcNow.Millisecond)
            //    Thread.Sleep(20);
        }
    }
}
