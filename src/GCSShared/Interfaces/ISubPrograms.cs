namespace GSendShared
{
    public interface ISubprograms
    {
        bool Exists(string name);

        bool Delete(string name);

        ISubProgram Get(string name);

        bool Update(ISubProgram subProgram);

        List<ISubProgram> GetAll();
    }
}
