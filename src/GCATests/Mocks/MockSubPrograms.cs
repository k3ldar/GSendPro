using System.Collections.Generic;
using System.Linq;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockSubprograms : ISubprograms
    {
        public bool Delete(string name)
        {
            ISubprogram subProgram = Subprograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();

            if (subProgram == null)
                return false;

            Subprograms.Remove(subProgram);
            return true;
        }

        public bool Exists(string name)
        {
            return Subprograms.Any(sp => sp.Name.Equals(name));
        }

        public ISubprogram Get(string name)
        {
            return Subprograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();
        }

        public List<ISubprogram> GetAll()
        {
            return Subprograms;
        }

        public bool Update(ISubprogram subProgram)
        {
            ISubprogram existing = Subprograms.Where(sp => sp.Name.Equals(subProgram.Name)).FirstOrDefault();

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
