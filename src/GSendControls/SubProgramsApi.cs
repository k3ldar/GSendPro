using System;
using System.Collections.Generic;
using System.Diagnostics;

using GSendApi;

using GSendShared;

namespace GSendControls
{
    public sealed class SubProgramsApi : ISubprograms
    {
        private readonly GSendApiWrapper _gSendApiWrapper;

        public SubProgramsApi(GSendApiWrapper gSendApiWrapper)
        {
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
        }

        public bool Delete(string name)
        {
            ValidateFileName(name);

            return _gSendApiWrapper.SubprogramDelete(name);
        }

        public bool Exists(string name)
        {
            try
            {
                ValidateFileName(name);
            }
            catch
            {
                return false;
            }

            return _gSendApiWrapper.SubprogramExists(name);
        }

        public ISubProgram Get(string name)
        {
            ValidateFileName(name);

            return _gSendApiWrapper.SubprogramGet(name);
        }

        public List<ISubProgram> GetAll()
        {
            return _gSendApiWrapper.SubprogramGet();
        }

        public bool Update(ISubProgram subProgram)
        {
            return _gSendApiWrapper.SubprogramUpdate(subProgram);
        }

        [DebuggerStepThrough]
        private static void ValidateFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (fileName[0] != 'O')
                throw new ArgumentException("Name must start with an O");

            string number = fileName[1..];

            if (!ushort.TryParse(number, out ushort value))
                throw new ArgumentException("File name has an invalid number");

            if (value < 1000 || value > 50000)
                throw new ArgumentException("Value must be between 1000 and 50000");
        }
    }
}
