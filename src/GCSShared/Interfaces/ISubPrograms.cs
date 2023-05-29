namespace GSendShared
{
    public interface ISubPrograms
    {
        bool Exists(string name);

        bool Delete(string name);

        ISubProgram Get(string name);

        bool Update(string name, string description, string content);

        List<ISubProgram> GetAll();
    }
}
