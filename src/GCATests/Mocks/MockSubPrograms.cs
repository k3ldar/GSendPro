using System.Collections.Generic;
using System.Linq;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockSubprograms : ISubprograms
    {
        public bool Delete(string name)
        {
            ISubprogram subProgram = Subprograms.Find(sp => sp.Name.Equals(name));

            if (subProgram == null)
                return false;

            Subprograms.Remove(subProgram);
            return true;
        }

        public bool Exists(string name)
        {
            return Subprograms.Exists(sp => sp.Name.Equals(name));
        }

        public ISubprogram Get(string name)
        {
            return Subprograms.Find(sp => sp.Name.Equals(name));
        }

        public List<ISubprogram> GetAll()
        {
            return Subprograms;
        }

        public bool Update(ISubprogram subProgram)
        {
            ISubprogram existing = Subprograms.Find(sp => sp.Name.Equals(subProgram.Name));

            if (existing == null)
                return false;

            existing.Name = subProgram.Name;
            existing.Description = subProgram.Description;
            existing.Contents = subProgram.Contents;
            return true;
        }

        public List<ISubprogram> Subprograms { get; set; } = new();
    }
}
