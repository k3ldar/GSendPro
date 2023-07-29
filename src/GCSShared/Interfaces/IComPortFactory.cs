using GSendShared.Abstractions;

namespace GSendShared
{
    public interface IComPortFactory
    {
        IComPort CreateComPort(IMachine machine);

        IComPort CreateComPort(IComPortModel model);

        IComPort GetComPort(string comPort);

        void DeleteComPort(IComPort comPort);
    }
}
