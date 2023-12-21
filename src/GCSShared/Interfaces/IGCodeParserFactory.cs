namespace GSendShared.Abstractions
{
    public interface IGCodeParserFactory
    {
        IGCodeParser CreateParser();

        IGCodeParser CreateParser(ISubprograms subprograms);
    }
}
