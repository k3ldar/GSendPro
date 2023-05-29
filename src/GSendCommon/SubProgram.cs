using GSendShared;

namespace GSendCommon
{
    internal sealed class SubProgram : ISubProgram
    {
        public SubProgram()
        {

        }

        public SubProgram(string name, string description, string contents)
        {
            Name = name;
            Description = description;
            Contents = contents;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Contents { get; set; }
    }
}
