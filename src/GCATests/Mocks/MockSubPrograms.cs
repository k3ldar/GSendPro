using System;
using System.Collections.Generic;
using System.Linq;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockSubPrograms : ISubPrograms
    {
        public bool Delete(string name)
        {
            return false;
        }

        public bool Exists(string name)
        {
            return SubPrograms.Any(sp => sp.Name.Equals(name));
        }

        public ISubProgram Get(string name)
        {
            return SubPrograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();
        }

        public List<ISubProgram> GetAll()
        {
            return SubPrograms;
        }

        public bool Update(ISubProgram subProgram)
        {
            throw new NotImplementedException();
        }

        public List<ISubProgram> SubPrograms { get; set; } = new();
    }
}
