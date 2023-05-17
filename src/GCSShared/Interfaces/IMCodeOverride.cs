namespace GSendShared.Interfaces
{
    public interface IMCodeOverride
    {
        bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken);
    }
}
