namespace GSendShared
{
    public interface ISubprograms
    {
        bool Exists(string name);

        bool Delete(string name);

        ISubprogram Get(string name);

        bool Update(ISubprogram subProgram);

        List<ISubprogram> GetAll();
    }
}
