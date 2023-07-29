namespace GSendShared.Abstractions
{
    public interface IMCodeOverride
    {
        bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken);
    }
}
