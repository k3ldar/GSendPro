namespace GSendShared
{
    public interface IComPortFactory
    {
        IComPort CreateComPort(IMachine machine);
    }
}
