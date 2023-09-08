using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using GSendApi;

using GSendShared;

using Shared.Classes;

namespace GSendControls
{
    public sealed class SubProgramsApi : ISubprograms
    {
        private readonly IGSendApiWrapper _gSendApiWrapper;
        private readonly CacheManager _subprogramCache = new(nameof(SubProgramsApi), TimeSpan.FromHours(2), true);

        public SubProgramsApi(IGSendApiWrapper gSendApiWrapper)
        {
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
        }

        public bool Delete(string name)
        {
            ValidateFileName(name);

            _subprogramCache.Clear();

            return _gSendApiWrapper.SubprogramDelete(name);
        }

        public bool Exists(string name)
        {
            try
            {
                ValidateFileName(name);

                return GetAll().Exists(i => i.Name == name);
            }
            catch
            {
                return false;
            }
        }

        public ISubprogram Get(string name)
        {
            ValidateFileName(name);
            string cacheName = $"{nameof(GetAll)} - {name}";

            CacheItem allSubprograms = _subprogramCache.Get(cacheName);

            if (allSubprograms == null)
            {
                allSubprograms = new(cacheName, _gSendApiWrapper.SubprogramGet(name));
                _subprogramCache.Add(cacheName, allSubprograms);
            }

            return (ISubprogram)allSubprograms.GetValue();
        }

        public List<ISubprogram> GetAll()
        {
            CacheItem allSubprograms = _subprogramCache.Get(nameof(GetAll));

            if (allSubprograms == null)
            {
                allSubprograms = new(nameof(GetAll), _gSendApiWrapper.SubprogramGet());
                _subprogramCache.Add(nameof(GetAll), allSubprograms);
            }

            return (List<ISubprogram>)allSubprograms.GetValue();
        }

        public bool Update(ISubprogram subProgram)
        {
            _subprogramCache.Clear();
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
