namespace GSendShared
{
    public interface ISubPrograms
    {
        bool Exists(string name);

        bool Delete(string name);

        ISubProgram Get(string name);

        bool Update(ISubProgram subProgram);

        List<ISubProgram> GetAll();
    }
}
