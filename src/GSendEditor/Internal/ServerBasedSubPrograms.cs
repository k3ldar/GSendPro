using GSendApi;

using GSendShared;

namespace GSendEditor.Internal
{
    internal sealed class ServerBasedSubPrograms : ISubprograms
    {
        private List<ISubprogram> _subPrograms;
        private readonly IGSendApiWrapper _gSendApiWrapper;

        public ServerBasedSubPrograms(IGSendApiWrapper gSendApiWrapper)
        {
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
            _gSendApiWrapper.ServerUriChanged += GSendApiWrapper_ServerUriChanged;
        }

        public bool Delete(string name)
        {
            try
            {
                if (_subPrograms == null)
                    _ = GetAll();

                return _gSendApiWrapper.SubprogramDelete(name);
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(string name)
        {
            try
            {
                if (_subPrograms == null)
                    _ = GetAll();

                return _subPrograms?.Exists(s => s.Name.Equals(name)) ?? false;
            }
            catch
            {
                return false;
            }
        }

        public ISubprogram Get(string name)
        {
            try
            {
                if (_subPrograms == null)
                    _ = GetAll();

                return _subPrograms?.Find(s => s.Name.Equals(name));
            }
            catch
            {
                return null;
            }
        }

        public List<ISubprogram> GetAll()
        {
            _subPrograms = _gSendApiWrapper.SubprogramGet();

            return _subPrograms ?? new();
        }

        public bool Update(ISubprogram subProgram)
        {
            try
            {
                _subPrograms = null;
                return _gSendApiWrapper.SubprogramUpdate(subProgram);
            }
            catch
            {
                return false;
            }
        }

        private void GSendApiWrapper_ServerUriChanged()
        {
            _subPrograms = null;
        }
    }
}
