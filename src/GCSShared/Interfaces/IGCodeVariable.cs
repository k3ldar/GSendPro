namespace GSendShared
{
    public interface IGCodeVariable
    {
        string VariableBlock { get; }

        List<string> Variables { get; }

        List<ushort> VariableIds { get; }
    }
}
