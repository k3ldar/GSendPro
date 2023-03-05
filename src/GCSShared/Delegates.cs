
namespace GSendShared
{
    public delegate void CommandSentHandler(object sender, CommandSent e);

    public delegate void GrblErrorHandler(object sender, GrblError errorCode);

    public delegate void BufferSizeHandler(object sender, int size);
}
