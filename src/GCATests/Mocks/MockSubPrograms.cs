using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockSubPrograms : ISubPrograms
    {
        public bool Delete(string name)
        {
            ISubProgram subProgram = SubPrograms.Where(sp => sp.Name.Equals(name)).FirstOrDefault();

            if (subProgram == null)
                return false;

            SubPrograms.Remove(subProgram);
            return true;
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
            ISubProgram existing = SubPrograms.Where(sp => sp.Name.Equals(subProgram.Name)).FirstOrDefault();

            if (existing == null)
                return false;

            existing.Name = subProgram.Name;
            existing.Description = subProgram.Description;
            existing.Contents = subProgram.Contents;
            return true;
        }

        public List<ISubProgram> SubPrograms { get; set; } = new();
    }
}
