using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon
{
    public sealed class SubProgramModel : ISubProgram
    {
        public SubProgramModel()
        {

        }

        public SubProgramModel(string name, string description, string contents)
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
