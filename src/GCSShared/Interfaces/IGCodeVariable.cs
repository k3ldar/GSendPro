namespace GSendShared
{
    public interface IGCodeVariable
    {
        string VariableBlock { get; }

        string Variable { get; }

        ushort VariableId { get; }
    }
}
