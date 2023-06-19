using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon
{
    public sealed class SubprogramModel : ISubProgram
    {
        public SubprogramModel()
        {

        }

        public SubprogramModel(string name, string description, string contents)
        {
            Name = name;
            Description = description;
            Contents = contents;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Contents { get; set; }

        public List<IGCodeVariable> Variables { get; set; }
    }
}
