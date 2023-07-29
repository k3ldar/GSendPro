using GSendShared;

namespace GSendApi
{
    public interface IGSendSubprogramApi
    {
        bool SubprogramDelete(string name);

        bool SubprogramExists(string name);

        List<ISubprogram> SubprogramGet();

        ISubprogram SubprogramGet(string name);

        bool SubprogramUpdate(ISubprogram subProgram);
    }
}
