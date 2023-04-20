namespace GSendShared.Interfaces
{
    public interface IOverriddenValue
    {
        int OriginalValue { get; }

        int NewValue { get; set; }
    }
}
